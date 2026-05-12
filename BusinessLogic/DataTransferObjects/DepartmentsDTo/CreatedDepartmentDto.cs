using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo
{
   public  class CreatedDepartmentDto
    {
		[Required(ErrorMessage = "Name Can not Be Null !! Required !!")]
		public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty!;
		public string? Description { get; set; }
		public DateOnly DateOfCreation { get; set; }
	}
}
