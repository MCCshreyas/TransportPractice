using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Employee.Components;
using MassTransit;

namespace TransportService
{
	public class Program
	{
		public static AppConfig AppConfig { get; set; }

		static async Task Main(string[] args)
		{
			var isService = !(Debugger.IsAttached || args.Contains("--console"));

			var builder = new HostBuilder()
				.ConfigureAppConfiguration((_, config) =>
				{
					config.AddJsonFile("appsettings.json", optional: true);
					config.AddEnvironmentVariables();

					if (args != null)
						config.AddCommandLine(args);
				})
				.ConfigureServices((hostContext, services) =>
				{
					AppConfig = new AppConfig();
					hostContext.Configuration.GetSection("AppConfig").Bind(AppConfig);

					services.AddMassTransit(config =>
					{
						config.AddConsumersFromNamespaceContaining<CreateEmployeeConsumer>();

						config.UsingRabbitMq((ctx, cfg) =>
						{
							cfg.Host(AppConfig.Host);
							cfg.ConfigureEndpoints(ctx);
						});
					});

					services.AddHostedService<MassTransitConsoleHostedService>();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
				});

			if (isService)
			{
				//await builder.UseWindowsService().Build().RunAsync();
				//await builder.UseSystemd().Build().RunAsync(); // For Linux, replace the nuget package: "Microsoft.Extensions.Hosting.WindowsServices" with "Microsoft.Extensions.Hosting.Systemd", and then use this line instead
			}
			else
			{
				await builder.RunConsoleAsync();
			}
		}
	}
}
