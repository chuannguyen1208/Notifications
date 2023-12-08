using Microsoft.AspNetCore.Components.Forms;

namespace Modules.Blog.Shared.Services;
public interface IFileService
{
	Task<string> UploadFile(IBrowserFile file);
}
