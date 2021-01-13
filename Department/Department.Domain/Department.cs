namespace Department.Domain
{
	public class Department
	{
		public int Id { get; set; }
		public string DepartmentName { get; set; }

		public int EmployeeCount { get; set; }

		public Department()
		{
			EmployeeCount = 0;
		}
	}
}