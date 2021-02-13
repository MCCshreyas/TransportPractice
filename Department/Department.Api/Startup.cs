using Department.Contracts.CreateDepartment;
using Department.Contracts.DeleteDepartment;
using Department.Contracts.GetAllDepartments;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedFramework;

namespace Department.Api
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
				config.AddRequestClient<CreateDepartmentRequest>();
				config.AddRequestClient<GetAllDepartmentRequest>();
				config.AddRequestClient<DeleteDepartmentRequest>();
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

			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepartmentApi"); });

			app.UseRouting();

			app.UseEndpoints(e => e.MapDefaultControllerRoute());
		}
	}
}