﻿using Microsoft.AspNetCore.Components.Forms;
using Modules.Blog.Shared.Services;

namespace Modules.Blog.Services;

internal class FileService : IFileService
{
	public Task<string> UploadFile(IBrowserFile file)
	{
		throw new NotImplementedException();
	}
}