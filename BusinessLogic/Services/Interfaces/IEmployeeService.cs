using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
	public interface IEmployeeService
	{
		IEnumerable<EmployeeDto> GetAllEmployees(string? SearchValue);
		bool DeleteEmployee(int id);
		EmployeeDetailsDto? GetEmployeebyId(int id);

		int CreateEmployee(CreatedEmployeeDto employeeDto);
		int UpdateEmployee(UpdatedEmployeeDto employeeDto);

	}
}
