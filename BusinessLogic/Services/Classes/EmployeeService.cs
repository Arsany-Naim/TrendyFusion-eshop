using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.BusinessLogic.Services.AttachementService;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
	public class EmployeeService(IUnitOfWork _unitOfWork  
		, IMapper _mapper , 
		IAttachementService _attachementService) : IEmployeeService
	{

		public IEnumerable<EmployeeDto> GetAllEmployees(string? SearchValue)
		{
			IEnumerable<Employee> employees;
			if (string.IsNullOrWhiteSpace(SearchValue))
				employees = _unitOfWork.EmployeeRepository.GetAll();
			else
				employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(SearchValue.ToLower()) && E.IsDeleted != true);

				var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
			return employeesDto;
		}
		public EmployeeDetailsDto? GetEmployeebyId(int id)
		{
			var employee = _unitOfWork.EmployeeRepository.GetById(id);
			return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
		}
		public int CreateEmployee(CreatedEmployeeDto employeeDto)
		{
			var employee = _mapper.Map<Employee>(employeeDto);
			if (employeeDto.Image is not null)
			{
				employee.Image = _attachementService.Upload(employeeDto.Image, "Images");
			}
			_unitOfWork.EmployeeRepository.Add(employee);
			return _unitOfWork.SaveChanges();
		}
		public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
		{
			 _unitOfWork.EmployeeRepository.Update(_mapper.Map<Employee>(employeeDto));
			return _unitOfWork.SaveChanges();
		}

		public bool DeleteEmployee(int id)
		{
			var employee = _unitOfWork.EmployeeRepository.GetById(id);
			if (employee is null) return false;
			else
			{
				employee.IsDeleted = true;
				_unitOfWork.EmployeeRepository.Update(employee);
				return _unitOfWork.SaveChanges() > 0 ? true : false;
			}
		}


	}
}
