using System.Threading.Tasks;
using MassTransit;
using TransportPractice.Contracts;

namespace TransportPractice.EventHandlers
{
	public class CustomerCreatedEventConsumer : IConsumer<CustomerCreatedEvent>
	{
		public Task Consume(ConsumeContext<CustomerCreatedEvent> context)
		{
			return Task.CompletedTask;
		}
	}

	public class AnotherCustomerCreatedEventConsumer : IConsumer<CustomerCreatedEvent>
	{
		public Task Consume(ConsumeContext<CustomerCreatedEvent> context)
		{
			return Task.CompletedTask;
		}
	}
}
