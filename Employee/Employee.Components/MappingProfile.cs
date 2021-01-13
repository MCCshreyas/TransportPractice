using AutoMapper;
using Employee.Contracts;

namespace Employee.Components
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Domain.Employee, EmployeeDto>();
		}
	}
}