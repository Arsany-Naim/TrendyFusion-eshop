using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachementService
{
	public class AttachementService : IAttachementService
	{
		private List<string> allowedExtensions = [".png", ".jpeg", ".jpg"];

		const int maxSize = 2_097_152; // 1024 * 1024 * 2  2MB 
		public string? Upload(IFormFile File, string FolderName)
		{
			// 1. Check Extension 
			var Extension = Path.GetExtension(File.FileName);
			if (!allowedExtensions.Contains(Extension))
				return null;

			// 2. Check Size 
			if (File == null || File.Length == 0 || File.Length > maxSize)
				return null;

			// 3. Get Located Folder Path
			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

			// 4. Make Attachement Name Unique -- GUID
			var FileName = $"{Guid.NewGuid()}_{File.FileName}";

			// 5. Get File Path
			var FilePath = Path.Combine(FolderPath, FileName); // File Location 

			// 6. Create File Stream To Copy File [Unmanaged]
			using FileStream fileStream = new FileStream(FilePath, FileMode.Create);

			// 7. Use Stream To Copy File 
			File.CopyTo(fileStream);
			
			// 8. Return FileName To Store In Database 
			return FileName;   
		}
		public bool Delete(string FilePath)
		{
			if (!File.Exists(FilePath)) return false;
			File.Delete(FilePath);
			return true;
		}
	}
}
