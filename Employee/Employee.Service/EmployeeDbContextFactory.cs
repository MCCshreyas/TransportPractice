using Employee.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharedFramework;
using System.IO;

namespace Employee.Service
{
	public class EmployeeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
	{
		public EmployeeDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			AppConfig config = new();
			configuration.GetSection("AppConfig").Bind(config);

			var builder = new DbContextOptionsBuilder<EmployeeDbContext>();
			builder.UseSqlServer(config.ConnectionString);
			return new EmployeeDbContext(builder.Options);
		}
	}
}