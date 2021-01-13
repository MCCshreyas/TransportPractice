namespace Employee.Contracts.CreateEmployee
{
	public record EmployeeCreatedEvent
	{
		public int EmployeeId { get; init; }
	}
}