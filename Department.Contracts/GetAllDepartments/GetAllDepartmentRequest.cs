using System.Collections.Generic;

namespace Department.Contracts.GetAllDepartments
{
	public record GetAllDepartmentRequest
	{
	}

	public record GetAllDepartmentResponse
	{
		public List<DepartmentDto> Departments { get; set; }
	}
}