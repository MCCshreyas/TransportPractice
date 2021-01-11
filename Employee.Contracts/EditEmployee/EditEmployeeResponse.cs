namespace Employee.Contracts.EditEmployee
{
	public record EditEmployeeResponse
	{
		public int Id { get; init; }
		public string Name { get; init; }
	}
}