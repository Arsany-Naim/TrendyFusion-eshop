using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly ApplicationDbContext _dbContext;
		private readonly Lazy<IDepartmentRepository> _departmentRepository;
		private readonly Lazy<IEmployeeRepository> _employeeRepository;

		public UnitOfWork(ApplicationDbContext applicationDbContext )
		{
			_dbContext = applicationDbContext;
			_departmentRepository = new Lazy<IDepartmentRepository>(()=> new DepartmentRepository(applicationDbContext));
			_employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(applicationDbContext)); ;	
		}

		public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

		public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

		public int SaveChanges() => _dbContext.SaveChanges();
	}
}
