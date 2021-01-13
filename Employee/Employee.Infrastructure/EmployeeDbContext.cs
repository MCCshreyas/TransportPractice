using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure
{
	public class EmployeeDbContext : DbContext
	{
		public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}

		public DbSet<Domain.Employee> Employees { get; set; }
	}
}