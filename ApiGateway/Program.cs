using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    var frameworkFolder = Path.Combine(env.ContentRootPath, "..","..","Common", "SharedFramework");
                    configuration.AddJsonFile(Path.Combine(frameworkFolder, "sharedSettings.json"), optional: true, reloadOnChange: true);
                    configuration.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
                });
    }
}
