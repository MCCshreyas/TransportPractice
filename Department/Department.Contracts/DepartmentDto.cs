namespace Department.Contracts.GetAllDepartments
{
	public record DepartmentDto
	{
		public int Id { get; set; }
		public string DepartmentName { get; set; }

		public int EmployeeCount { get; set; }
	}
}