using System.IO;
using Department.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharedFramework;

namespace Department.Service
{
	public class DepartmentDbContextFactory : IDesignTimeDbContextFactory<DepartmentDbContext>
	{
		public DepartmentDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			AppConfig config = new();
			configuration.GetSection("AppConfig").Bind(config);

			var builder = new DbContextOptionsBuilder<DepartmentDbContext>();
			builder.UseSqlServer(config.ConnectionString);
			return new DepartmentDbContext(builder.Options);
		}
	}
}