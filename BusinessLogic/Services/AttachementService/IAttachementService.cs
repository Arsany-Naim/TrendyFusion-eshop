using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachementService
{
	public interface IAttachementService
	{
		string? Upload(IFormFile File, string FolderName);
		bool Delete(string FilePath);
	}
}
