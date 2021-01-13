using Employee.Contracts.CreateEmployee;
using Employee.Infrastructure;
using MassTransit;
using System.Threading.Tasks;

namespace Employee.Components
{
	public class CreateEmployeeConsumer : IConsumer<CreateEmployeeRequest>
	{
		private readonly EmployeeDbContext _context;

		public CreateEmployeeConsumer(EmployeeDbContext context)
		{
			_context = context;
		}

		public async Task Consume(ConsumeContext<CreateEmployeeRequest> consumeContext)
		{
			var employee = new Domain.Employee
			{
				Name = consumeContext.Message.Name
			};

			await _context.Employees.AddAsync(employee);
			await _context.SaveChangesAsync();

			await consumeContext.Publish(new EmployeeCreatedEvent {EmployeeId = employee.Id});

			await consumeContext.RespondAsync(new CreateEmployeeResponse
			{
				Name = consumeContext.Message.Name,
				Id = employee.Id
			});
		}
	}
}