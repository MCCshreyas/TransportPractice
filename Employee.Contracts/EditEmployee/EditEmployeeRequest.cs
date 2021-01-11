using System.ComponentModel.DataAnnotations;

namespace Employee.Contracts.EditEmployee
{
	public record EditEmployeeRequest
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; init; }
	}
}
