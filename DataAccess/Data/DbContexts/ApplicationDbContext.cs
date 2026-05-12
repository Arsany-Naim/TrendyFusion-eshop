using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection;

namespace Demo.DataAccess.Data.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

		
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("");
		//}


		public DbSet<Department> Departments { get; set; }

		public DbSet<Employee> Employees { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<BaseEntity>();

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			//modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
