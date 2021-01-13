using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Employee.Contracts.CreateEmployee;
using Employee.Contracts.EditEmployee;
using Employee.Contracts.GetAllEmployee;
using MassTransit;
using Microsoft.Extensions.Configuration;
using SharedFramework;

namespace Employee.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var appConfig = new AppConfig();
			Configuration.GetSection(nameof(AppConfig)).Bind(appConfig);

			services.AddSwaggerGen();
			services.AddMassTransit(config =>
			{
				config.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host(appConfig.Host);
					cfg.ConfigureEndpoints(ctx);
				});
				config.AddRequestClient<CreateEmployeeRequest>();
				config.AddRequestClient<GetAllEmployeeRequest>();
				config.AddRequestClient<EditEmployeeRequest>();
			});

			services.AddMassTransitHostedService();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeApi");
			});

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
