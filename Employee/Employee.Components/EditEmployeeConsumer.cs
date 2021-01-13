using Employee.Contracts.EditEmployee;
using Employee.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Components
{
	public class EditEmployeeConsumer : IConsumer<EditEmployeeRequest>
	{
		private readonly EmployeeDbContext _context;

		public EditEmployeeConsumer(EmployeeDbContext context)
		{
			_context = context;
		}

		public async Task Consume(ConsumeContext<EditEmployeeRequest> consumerContext)
		{
			var dbEmployee = _context.Employees.FirstOrDefault(x => x.Id == consumerContext.Message.Id);

			if (dbEmployee != null)
			{
				dbEmployee.Name = consumerContext.Message.Name;

				_context.Entry(dbEmployee).State = EntityState.Modified;

				await _context.SaveChangesAsync();

				await consumerContext.RespondAsync(new EditEmployeeResponse
				{
					Id = dbEmployee.Id,
					Name = dbEmployee.Name
				});
			}
		}
	}
}