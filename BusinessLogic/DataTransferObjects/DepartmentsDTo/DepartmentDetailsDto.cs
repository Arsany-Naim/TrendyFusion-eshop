using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.DepartmentsDTo
{
  public  class DepartmentDetailsDto
    {
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty!;
		public string? Description { get; set; }
		public DateOnly DateOfCreation { get; set; }
		public int CreatedBy { get; set; } 
		public int LastModifiedBy { get; set; }
		public DateOnly LastModifiedOn { get; set; }
		public bool IsDeleted { get; set; }  
	}
}
