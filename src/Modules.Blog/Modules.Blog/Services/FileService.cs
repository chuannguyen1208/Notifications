using Microsoft.AspNetCore.Components.Forms;
using Modules.Blog.Shared.Services;

namespace Modules.Blog.Services;

internal class FileService(IWebHostEnvironment env) : IFileService
{
	public async Task<string> UploadFile(IBrowserFile file)
	{
		var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.Name));
		var returnFilePath = Path.Combine("img", "temp", fileName);
		var filePath = Path.Combine(env.ContentRootPath, "wwwroot", returnFilePath);
		var dir = Path.GetDirectoryName(filePath);
		Directory.CreateDirectory(dir!);

		await using FileStream fs = new(filePath, FileMode.Create);
		await file.OpenReadStream().CopyToAsync(fs);

		return returnFilePath;
	}
}
