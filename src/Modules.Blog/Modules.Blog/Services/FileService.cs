using Microsoft.AspNetCore.Components.Forms;
using Modules.Blog.Shared.Services;

namespace Modules.Blog.Services;

internal class FileService(IWebHostEnvironment env, IUserService user) : IFileService
{
	public async Task<string> UploadFile(IBrowserFile file, long maxFileSize = 15360)
	{
		var userAuthenticated = await user.IsAuthenticated().ConfigureAwait(false);
		if (!userAuthenticated)
		{
			throw new InvalidOperationException("User must be logged in to upload file.");
		}

		var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.Name));
		var returnFilePath = Path.Combine("img", "temp", fileName);
		var filePath = Path.Combine(env.ContentRootPath, "wwwroot", returnFilePath);
		var dir = Path.GetDirectoryName(filePath);
		Directory.CreateDirectory(dir!);

		await using FileStream fs = new(filePath, FileMode.Create);
		await file.OpenReadStream(maxFileSize).CopyToAsync(fs);

		return returnFilePath;
	}

}
