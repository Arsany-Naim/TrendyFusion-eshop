using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations.EmployeeConfig
{
	public class EmployeeConfigurations : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
	{
		public new void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Name).HasColumnType("varchar(50)");
			builder.Property(E => E.Address).HasColumnType("varchar(100)");
			builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
			builder.Property(E => E.Gender)
				.HasConversion((Gender) => Gender.ToString(), // Converts Enum to string before storing in DB
				(gender) => (Gender) Enum.Parse(typeof(Gender) , gender)); // // Converts string back to Enum when retrieving from DB

			builder.Property(E => E.EmployeeType)
				.HasConversion((type) => type.ToString(), // Converts Enum to string before storing in DB
				(EmpType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmpType)); // // Converts string back to Enum when retrieving from DB
			
			base.Configure(builder);
		}
	}
}
