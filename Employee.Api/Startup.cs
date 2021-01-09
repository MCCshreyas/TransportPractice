using Employee.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Employee.Contracts;
using Employee.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen();
			services.AddMassTransit(config =>
			{
				config.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host("localhost");
					cfg.ConfigureEndpoints(ctx);
				});
				config.AddRequestClient<CreateEmployeeRequest>();
			});

			services.AddMassTransitHostedService();
			services.AddControllers();
			services.AddDbContext<EmployeeDbContext>(
				option =>
				{
					option.UseInMemoryDatabase("EmployeeDb");
				});
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
