using System.ComponentModel.DataAnnotations;

namespace Employee.Contracts
{
	public class CreateEmployeeRequest
	{
		[Required]
		public string Name { get; set; }
	}

	public class CreateEmployeeResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class EmployeeCreatedEvent
	{
		public int EmployeeId { get; set; }
	}
}
