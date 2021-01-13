using System.ComponentModel.DataAnnotations;

namespace Employee.Contracts.CreateEmployee
{
	public record CreateEmployeeRequest
	{
		[Required] public string Name { get; init; }
	}
}