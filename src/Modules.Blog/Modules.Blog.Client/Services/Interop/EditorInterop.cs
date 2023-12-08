using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class EditorInterop
{
	private readonly Lazy<Task<IJSObjectReference>> moduleTask;

	public EditorInterop(IJSRuntime js)
	{
		moduleTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>("import", "./js/editor.js").AsTask());
	}

	public async ValueTask LoadEditorAsync(ElementReference textareaElement, ElementReference inputFileElement, string toolbar = "miniToolbar")
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("loadEditor", textareaElement, inputFileElement, toolbar);
	}

	public async ValueTask WriteFrontFileAsync(ElementReference? imageUpload)
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("writeFrontFile", imageUpload);
	}

	public async ValueTask WriteFrontFileTempAsync(string filename, string url)
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("writeFrontFileTemp", filename, url);
	}

	public async ValueTask SetEditorValueAsync(string content)
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("setEditorValue", content);
	}

	public async ValueTask<string> GetEditorValueAsync()
	{
		var module = await moduleTask.Value;
		var content = await module.InvokeAsync<string>("getEditorValue");
		return content;
	}
}
