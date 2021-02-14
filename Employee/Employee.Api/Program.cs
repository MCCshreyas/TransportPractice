using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Employee.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateLogger();

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    var frameworkFolder = Path.Combine(env.ContentRootPath, "..","..","Common", "SharedFramework");
                    config.AddJsonFile(Path.Combine(frameworkFolder, "sharedSettings.json"), optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables();

					if (args != null)
						config.AddCommandLine(args);
                })
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}