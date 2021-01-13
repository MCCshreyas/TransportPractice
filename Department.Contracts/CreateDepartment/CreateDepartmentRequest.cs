using System.ComponentModel.DataAnnotations;

namespace Department.Contracts.CreateDepartment
{
	public class CreateDepartmentRequest
	{
		[Required]
		public string Name { get; set; }
	}

	public class CreateDepartmentResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
