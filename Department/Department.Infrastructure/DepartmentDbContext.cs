using Microsoft.EntityFrameworkCore;

namespace Department.Infrastructure
{
	public class DepartmentDbContext : DbContext
	{
		public DepartmentDbContext(DbContextOptions<DepartmentDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}

		public DbSet<Domain.Department> Departments { get; set; }
	}
}
