using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransportPractice.Consumers;
using TransportPractice.Contracts;
using TransportPractice.EventHandlers;

namespace TransportPractice
{
	public class Startup
	{
		public readonly IConfiguration Configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddMassTransit(config =>
			{
				config.AddConsumersFromNamespaceContaining<CreateCustomerConsumer>();
				config.AddConsumersFromNamespaceContaining<AnotherCustomerCreatedEventConsumer>();

				config.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host(Configuration["RabbitMqHostURL"]);
					cfg.ConfigureEndpoints(ctx);
				});

				config.AddRequestClient<CreateCustomerRequest>();
			});

			services.AddSwaggerGen();
			services.AddMassTransitHostedService();
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
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transport practice");
			});

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
