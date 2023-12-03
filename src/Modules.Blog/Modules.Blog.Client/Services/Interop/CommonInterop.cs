using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class CommonInterop(IJSRuntime Js)
{
	public async Task<string> BlogUrlToBase64(string blobUrl)
	{
		return await Js.InvokeAsync<string>("convertBlobURLToBase64", blobUrl);
	}
}
