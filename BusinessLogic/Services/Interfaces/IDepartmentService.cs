using Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo;

namespace Demo.BusinessLogic.Services.Interfaces
{
	public interface IDepartmentService
	{
		int CreateDepartment(CreatedDepartmentDto departmentDto);
		bool DeleteDepartment(int id);
		IEnumerable<DepartmentDto> GetAllDepartments();
		DepartmentDetailsDto? GetDepartmentById(int id);
		int UpdateDepartment(UpdatedDepartmentDto departmentDto);
	}
}