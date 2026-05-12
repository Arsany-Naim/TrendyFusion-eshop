using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo;
using Demo.BusinessLogic.DataTransferObjects.EmployeesDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Presentation.Controllers
{
	[Authorize]
    public class EmployeesController(IEmployeeService _employeeService ,
		IWebHostEnvironment environment ,
		ILogger<EmployeesController> _logger) : Controller
    {
        // GET : BaseURl/Employees/Index
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }


		#region Create New Employee 
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
			if(ModelState.IsValid)
            {
				try
				{
					var employeeDto = new CreatedEmployeeDto()
					{
						Name = employeeViewModel.Name , 
						Age = employeeViewModel.Age,
						DepartmentId = employeeViewModel.DepartmentId,
						Email = employeeViewModel.Email,
						EmployeeType = employeeViewModel.EmployeeType ,
						Gender = employeeViewModel.Gender,
						HiringDate = employeeViewModel.HiringDate,
						IsActive = employeeViewModel.IsActive ,
						PhoneNumber = employeeViewModel.PhoneNumber,
						Salary = employeeViewModel.Salary,
						Address = employeeViewModel.Address ,
						Image = employeeViewModel.Image
					};
					int Result = _employeeService.CreateEmployee(employeeDto);
					if (Result > 0)
						return RedirectToAction(nameof(Index));
					else
					{
						ModelState.AddModelError(string.Empty, "Can't Create Employee");
					}
				}
				catch (Exception ex)
				{

					if (environment.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
					{
						_logger.LogError(ex.Message);
						return View("ErrorView");
					}
				}
			}

			return View(employeeViewModel);

		}
		#endregion

		#region Details Of Employee

		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var employee = _employeeService.GetEmployeebyId(id.Value);

			return employee is not null ? View(employee) : NotFound();
		}

		#endregion

		#region Update Employee 

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var employee = _employeeService.GetEmployeebyId(id.Value);
			if (employee is null) return NotFound();
			var employeeViewModel = new EmployeeViewModel()
			{
				Address = employee.Address,
				Name = employee.Name,
				Age = employee.Age,
				Email = employee.Email,
				HiringDate = employee.HiringDate,
				IsActive = employee.IsActive,
				PhoneNumber = employee.PhoneNumber,
				Salary = employee.Salary,
				EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
				Gender = Enum.Parse<Gender>(employee.Gender)
			};
			 return View(employeeViewModel);
		}

		[HttpPost]
		public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
		{
			if (!id.HasValue ) return BadRequest();
			if (!ModelState.IsValid) return View(employeeViewModel);
			try
			{

				var employeeDto = new UpdatedEmployeeDto()
				{
					Id  = id.Value,
					Address = employeeViewModel.Address,
					Name = employeeViewModel.Name,
					Age = employeeViewModel.Age,
					Email = employeeViewModel.Email,
					HiringDate = employeeViewModel.HiringDate,
					IsActive = employeeViewModel.IsActive,
					PhoneNumber = employeeViewModel.PhoneNumber,
					Salary = employeeViewModel.Salary,
					EmployeeType = employeeViewModel.EmployeeType,
					Gender = employeeViewModel.Gender,
					DepartmentId = employeeViewModel.DepartmentId
				};
				var Result = _employeeService.UpdateEmployee(employeeDto);
				if (Result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					ModelState.AddModelError(string.Empty, "Employee is not Updated");
					return View(employeeViewModel);
				}
			}
			catch (Exception ex)
			{

				if (environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return View(employeeViewModel);
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("ErrorView", ex);
				}
			}
		}
		#endregion

		#region Delete Employee
		[HttpPost] 
		public IActionResult Delete(int id)
		{
			if (id == 0) return BadRequest();
			try
			{
				var Deleted = _employeeService.DeleteEmployee(id);
				if (Deleted)
					return RedirectToAction(nameof(Index));
				else
				{
					return RedirectToAction(nameof(Index));
					// With Message That Employee Is Not Deleted 
				}
			}
			catch (Exception ex)
			{

				if (environment.IsDevelopment())
				{
					return RedirectToAction(nameof(Index));
					// With Message That Employee Not Deleted And Exception
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("ErrorView", ex);
				}
			}
		}

		#endregion
	}
}
