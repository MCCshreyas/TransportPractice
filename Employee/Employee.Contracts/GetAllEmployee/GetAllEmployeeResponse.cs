using System.Collections.Generic;

namespace Employee.Contracts.GetAllEmployee
{
	public record GetAllEmployeeResponse
	{
		public IList<EmployeeDto> Employee { get; set; }
	}
}