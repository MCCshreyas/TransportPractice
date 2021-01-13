using Department.Contracts.CreateDepartment;
using Department.Infrastructure;
using MassTransit;
using System.Threading.Tasks;

namespace Department.Components.Consumers
{
	public class CreateDepartmentConsumer : IConsumer<CreateDepartmentRequest>
	{
		private readonly DepartmentDbContext _dbContext;

		public CreateDepartmentConsumer(DepartmentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Consume(ConsumeContext<CreateDepartmentRequest> context)
		{
			var department = new Domain.Department
			{
				DepartmentName = context.Message.Name
			};

			await _dbContext.Departments.AddAsync(department);
			await _dbContext.SaveChangesAsync();

			await context.RespondAsync(new CreateDepartmentResponse
			{
				Name = context.Message.Name,
				Id = department.Id
			});
		}
	}
}