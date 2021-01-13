using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Department.Contracts.GetAllDepartments;
using Department.Infrastructure;
using MassTransit;

namespace Department.Components.Consumers
{
	public class GetAllDepartmentConsumer : IConsumer<GetAllDepartmentRequest>
	{
		private readonly DepartmentDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllDepartmentConsumer
			(DepartmentDbContext dbContext,
			IMapper mapper
		)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Consume(ConsumeContext<GetAllDepartmentRequest> context)
		{
			var departments = _dbContext.Departments.ToList();

			var allDepartmentDtos =  _mapper.Map<List<Domain.Department>,List<DepartmentDto>>(departments);

			await context.RespondAsync(new GetAllDepartmentResponse
			{
				Departments = allDepartmentDtos
			});
		}
	}
}