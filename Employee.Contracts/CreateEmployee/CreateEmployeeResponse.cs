namespace Employee.Contracts.CreateEmployee
{
	public record CreateEmployeeResponse
	{
		public int Id { get; init; }
		public string Name { get; init; }
	}
}