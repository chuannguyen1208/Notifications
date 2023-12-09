using Microsoft.AspNetCore.Components.Forms;
using Modules.Blog.Shared.Services;

namespace Modules.Blog.Client.Services;

internal class FileService : IFileService
{
	public Task<string> UploadFile(IBrowserFile file, long maxFileSize = 15360)
	{
		throw new NotImplementedException();
	}
}
