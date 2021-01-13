using AutoMapper;
using Employee.Contracts;
using Employee.Contracts.GetAllEmployee;
using Employee.Infrastructure;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Components
{
	public class GetAllEmployeeConsumer : IConsumer<GetAllEmployeeRequest>
	{
		private readonly EmployeeDbContext _context;
		private readonly IMapper _mapper;

		public GetAllEmployeeConsumer(EmployeeDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Consume(ConsumeContext<GetAllEmployeeRequest> context)
		{
			var employees = _context.Employees.ToList();

			var allEmployee = _mapper.Map<List<Domain.Employee>, List<EmployeeDto>>(employees);

			await context.RespondAsync(new GetAllEmployeeResponse {Employee = allEmployee});
		}
	}
}