using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService department 
        , ILogger<DepartmentsController> _logger ,
        IWebHostEnvironment environment) : Controller
    {
		private readonly IDepartmentService _departmentService = department;

		public IActionResult Index()
		{
			ViewData["Message"] = new DepartmentDto() { Name = "TestViewData" };
			ViewBag.Message = new DepartmentDto() { Name = "TestViewBag" };
			var departments = _departmentService.GetAllDepartments();
			return View(departments);
		}

		#region Create Department 

		[HttpGet]
		public IActionResult Create() => View();


		[HttpPost]
		//[ValidateAntiForgeryToken] Action Filter
		public IActionResult Create(DepartmentViewModel departmentViewModel)
		{
			if (ModelState.IsValid) // Server Side Validation
			{
				try
				{
					var departmentDto = new CreatedDepartmentDto()
					{
						Name = departmentViewModel.Name,
						Code = departmentViewModel.Code,
						DateOfCreation = departmentViewModel.DateOfCreation,
						Description = departmentViewModel.Description
					};
					int Result = _departmentService.CreateDepartment(departmentDto);
					string Message;
					if (Result > 0)
						Message = $"Department {departmentViewModel.Name} is Created Successfully";
					else
						Message = $"Department {departmentViewModel.Name} can not be Created ";
					TempData["Message"] = Message;
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{

					if (environment.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						_logger.LogError(ex.Message);
				}
			}
			return View(departmentViewModel);
		}
		#endregion

		#region Details Of Department

		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (!id.HasValue) return BadRequest(); // 400
			var department = _departmentService.GetDepartmentById(id.Value);
			if (department is null) return NotFound(); // 404
			return View(department);
		}


		#endregion

		#region Edit Department 
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (!id.HasValue) 
				return BadRequest();
			var department = _departmentService.GetDepartmentById(id.Value);
			if (department is null) 
				return NotFound();
			var departmentViewModel = new DepartmentViewModel()
			{
				Code = department.Code,
				DateOfCreation = department.DateOfCreation,
				Description = department.Description,
				Name = department.Name
			};
			return View(departmentViewModel);
		}

		[HttpPost]
		public IActionResult Edit([FromRoute] int? id, DepartmentViewModel departmentViewModel)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return View(departmentViewModel);
			try
			{
				var UpdatedDepartment = new UpdatedDepartmentDto()
				{
					Id = id.Value,
					Code = departmentViewModel.Code,
					DateOfCreation = departmentViewModel.DateOfCreation,
					Description = departmentViewModel.Description,
					Name = departmentViewModel.Name
				};
				var Result = _departmentService.UpdateDepartment(UpdatedDepartment);
				if (Result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					ModelState.AddModelError(string.Empty, "Department not Updated");
					return View(departmentViewModel);
				}
			}
			catch (Exception ex)
			{

				if (environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return View(departmentViewModel);
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("Error", ex);
				}
			}
		}
		#endregion

		#region Delete Department 
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (!id.HasValue) return BadRequest();

			var department = _departmentService.GetDepartmentById(id.Value);
			if (department is null) return NotFound();

			return View(department);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			if (id == 0) return BadRequest();
			try
			{
			var Deleted = _departmentService.DeleteDepartment(id);
				if (Deleted)
					return RedirectToAction(nameof(Index));
				else
				{
					ModelState.AddModelError(string.Empty, "Department not Deleted");
					return RedirectToAction(nameof(Delete), new { id = id });
				}
			}
			catch (Exception ex)
			{

				if (environment.IsDevelopment())
				{
					return RedirectToAction(nameof(Index));
					// With Message That Department Not Deleted  
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("Error", ex);
				}
			}
		}
		#endregion
	}
}
