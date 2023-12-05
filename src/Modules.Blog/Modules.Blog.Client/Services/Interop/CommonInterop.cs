using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class CommonInterop(IJSRuntime Js)
{
	public async Task<string> BlogUrlToBase64(string blobUrl)
	{
		return await Js.InvokeAsync<string>("convertBlobURLToBase64", blobUrl);
	}

	public async Task<string> SetInnerHtml(ElementReference element, string html)
	{
		return await Js.InvokeAsync<string>("setInnerHtml", element, html);
	}

	public async Task Toast(string text)
	{
		await Js.InvokeVoidAsync("toast", text);
	}

	public async Task ToastSuccess(string text)
	{
		await Js.InvokeVoidAsync("toastSuccess", text);
	}

	public async Task ToastError(string text)
	{
		await Js.InvokeVoidAsync("toastError", text);
	}
}
