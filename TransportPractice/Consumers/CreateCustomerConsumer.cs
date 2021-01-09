using System;
using System.Threading.Tasks;
using MassTransit;
using TransportPractice.Contracts;

namespace TransportPractice.Consumers
{
	public class CreateCustomerConsumer : IConsumer<CreateCustomerRequest>
	{
		public async Task Consume(ConsumeContext<CreateCustomerRequest> context)
		{
			var customerId = Guid.NewGuid();

			await context.RespondAsync(new CreateCustomerResponse { Id = customerId, Name = context.Message.Name });

			await context.Publish(new CustomerCreatedEvent {  Id = customerId });
		}
	}
}
