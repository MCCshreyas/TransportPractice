using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Employee.Components;
using Employee.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Serilog;
using SharedFramework;

namespace TransportService
{
	public class Program
	{
		public static AppConfig AppConfig { get; set; }

		static async Task Main(string[] args)
		{
			var isService = !(Debugger.IsAttached || args.Contains("--console"));


			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateLogger();

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

					services.AddAutoMapper(typeof(CreateEmployeeConsumer).Assembly);

					services.AddMassTransit(config =>
					{
						config.AddConsumersFromNamespaceContaining(typeof(CreateEmployeeConsumer));

						config.UsingRabbitMq((ctx, cfg) =>
						{
							cfg.Host(AppConfig.Host);
							cfg.ConfigureEndpoints(ctx);
						});
					});

					services.AddDbContext<EmployeeDbContext>(
					option =>
					{
						option.UseSqlServer(AppConfig.ConnectionString, a =>
						{
							a.MigrationsAssembly(typeof(EmployeeDbContext).FullName);
						});
					});

					services.AddHostedService<MassTransitConsoleHostedService>();
				}).UseSerilog()
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

	public class EmployeeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
	{
		public EmployeeDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			AppConfig config = new AppConfig();
			configuration.GetSection("AppConfig").Bind(config);

			var builder = new DbContextOptionsBuilder<EmployeeDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			builder.UseSqlServer(config.ConnectionString);
			return new EmployeeDbContext(builder.Options);
		}
	}

}
