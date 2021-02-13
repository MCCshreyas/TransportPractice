using System.Linq;
using System.Threading.Tasks;
using Employee.Contracts.DeleteEmployee;
using Employee.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Employee.Components
{
	public class DeleteEmployeeConsumer : IConsumer<DeleteEmployeeRequest>
	{
		private readonly EmployeeDbContext _dbContext;

		public DeleteEmployeeConsumer(EmployeeDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Consume(ConsumeContext<DeleteEmployeeRequest> context)
		{
			var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == context.Message.Id);

			if (employee is not null)
			{
				_dbContext.Entry(employee).State = EntityState.Deleted;
				await _dbContext.SaveChangesAsync();

				await context.RespondAsync(new DeleteEmployeeResponse
				{
					Id = context.Message.Id,
					IsDeleted = true
				});
			}

			await context.RespondAsync(new DeleteEmployeeResponse
			{
				Id = context.Message.Id,
				IsDeleted = false
			});
		}
	}
}