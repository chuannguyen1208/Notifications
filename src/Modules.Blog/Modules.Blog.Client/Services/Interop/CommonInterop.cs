﻿using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class CommonInterop
{
	private readonly Lazy<Task<IJSObjectReference>> moduleTask;

	public CommonInterop(IJSRuntime js)
	{
		moduleTask = new(() => js.InvokeAsync<IJSObjectReference>("import", "./assets/js/common.js").AsTask());
	}

	public async Task<string> BlogUrlToBase64(string blobUrl)
	{
		var module = await moduleTask.Value;
		return await module.InvokeAsync<string>("convertBlobURLToBase64", blobUrl);
	}

	public async Task ToastInfo(string text)
	{
		await Toast(text, "text-primary");
	}

	public async Task ToastSuccess(string text)
	{
		await Toast(text, "text-success");
	}

	public async Task ToastError(string text)
	{
		await Toast(text, "text-danger");
	}

	private async Task Toast(string text, string type = "", int closeAfterMs = 3000)
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("toast", text, type, closeAfterMs);
	}
}
