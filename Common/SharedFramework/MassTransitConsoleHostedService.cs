﻿using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SharedFramework
{
	public class MassTransitConsoleHostedService : IHostedService
	{
		private readonly IBusControl _bus;
		private readonly ILogger _logger;

		public MassTransitConsoleHostedService(IBusControl bus, ILoggerFactory loggerFactory)
		{
			_bus = bus;
			_logger = loggerFactory.CreateLogger<MassTransitConsoleHostedService>();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Bus is starting...");
			await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Bus is stopping...");
			return _bus.StopAsync(cancellationToken);
		}
	}
}