using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
		public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
		{
			
		}

	}
}
