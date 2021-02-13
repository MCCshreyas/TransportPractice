using AutoMapper;
using Department.Contracts.GetAllDepartments;

namespace Department.Components
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Domain.Department, DepartmentDto>();
		}
	}
}