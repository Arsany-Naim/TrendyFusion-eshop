using Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo;
using Demo.DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
	public static class DepartmentFactory
	{
		// Extension method
		public static DepartmentDetailsDto ToDetailsDto(this Department department) => new DepartmentDetailsDto()
		{
			Id = department.Id,
			Name = department.Name,
			Code = department.Code,
			CreatedBy = department.CreatedBy,
			DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
			Description = department.Description,
			IsDeleted = department.IsDeleted,
			LastModifiedBy = department.LastModifiedBy,
			LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
		};

		public static DepartmentDto ToDto(this Department department) => new ()
		{
			Id = department.Id,
			Name = department.Name,
			Code = department.Code,
			DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
			Description = department.Description
		};


		public static Department ToEntity(this CreatedDepartmentDto departmentDto) => new Department()
		{
			Name = departmentDto.Name ,
			Code = departmentDto.Code,
			CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()) , 
			Description = departmentDto.Description
		};

		public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
		{
			Id = departmentDto.Id,
			Name = departmentDto.Name,
			Code = departmentDto.Code,
			CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
			Description = departmentDto.Description
		};

	}
}
