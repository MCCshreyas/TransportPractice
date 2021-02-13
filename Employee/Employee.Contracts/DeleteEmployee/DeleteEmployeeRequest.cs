namespace Employee.Contracts.DeleteEmployee
{
	public record DeleteEmployeeRequest
	{
		public int Id { get; set; }
	}

	public record DeleteEmployeeResponse
	{
		public int Id { get; set; }
		public bool IsDeleted { get; set; }
	}
}