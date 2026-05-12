using Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
	public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
	{

		// Get All Departments 
		public IEnumerable<DepartmentDto> GetAllDepartments()
		{
			var Departments = unitOfWork.DepartmentRepository.GetAll();

			return Departments.Select(D => D.ToDto());
		}


		// Get Department By Id 
		public DepartmentDetailsDto? GetDepartmentById(int id)
		{
			var department = unitOfWork.DepartmentRepository.GetById(id);

			// Manual Mapping 
			return department?.ToDetailsDto();
		}

		// Create New Department 
		public int CreateDepartment(CreatedDepartmentDto departmentDto)
		{
			var department = departmentDto.ToEntity();
			 unitOfWork.DepartmentRepository.Add(department);
			return unitOfWork.SaveChanges();
		}

		// Updated Department 
		public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
		{
			var department = departmentDto.ToEntity();
			 unitOfWork.DepartmentRepository.Update(department);
			return unitOfWork.SaveChanges();

		}

		// Delete Department 

		public bool DeleteDepartment(int id)
		{
			var department = unitOfWork.DepartmentRepository.GetById(id);
			if (department is null) return false;
			else
			{
				unitOfWork.DepartmentRepository.Remove(department);
				int Result = unitOfWork.SaveChanges();
				if (Result > 0) return true;
				else return false;
			}
		}
	}
}
