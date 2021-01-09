using System;

namespace TransportPractice.Contracts
{
	public record CreateCustomerRequest
	{
		public string Name { get; set; }
	}

	public record CreateCustomerResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

	public record CustomerCreatedEvent
	{
		public Guid Id { get;set; }
	}
}