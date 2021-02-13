using System.Linq;
using System.Threading.Tasks;
using Department.Contracts.DeleteDepartment;
using Department.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Department.Components.Consumers
{
	public class DeleteDepartmentConsumer : IConsumer<DeleteDepartmentRequest>
	{
		private readonly DepartmentDbContext _dbContext;

		public DeleteDepartmentConsumer(DepartmentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Consume(ConsumeContext<DeleteDepartmentRequest> context)
		{
			var department = _dbContext.Departments.FirstOrDefault(x => x.Id == context.Message.DepartmentId);

			if (department != null && department.EmployeeCount == 0)
			{
				_dbContext.Entry(department).State = EntityState.Deleted;
				await _dbContext.SaveChangesAsync();

				await context.RespondAsync(new DeleteDepartmentResponse
				{
					DepartmentId = context.Message.DepartmentId,
					IsDeleted = true
				});
			}

			await context.RespondAsync(new DeleteDepartmentResponse
			{
				DepartmentId = context.Message.DepartmentId,
				IsDeleted = false
			});
		}
	}
}