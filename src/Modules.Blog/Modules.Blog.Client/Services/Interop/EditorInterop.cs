using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class EditorInterop(IJSRuntime JS)
{
	public async ValueTask LoadEditorAsync(ElementReference textareaElement, ElementReference inputFileElement, string toolbar = "miniToolbar")
	{
		await JS.InvokeVoidAsync("EditorMDE.loadEditor", textareaElement, inputFileElement, toolbar);
	}

	public async ValueTask WriteFrontFileAsync(ElementReference? imageUpload)
	{
		await JS.InvokeVoidAsync("EditorMDE.writeFrontFile", imageUpload);
	}

	public async ValueTask SetEditorValueAsync(string content)
	{
		await JS.InvokeVoidAsync("EditorMDE.setEditorValue", content);
	}

	public async ValueTask<string> GetEditorValueAsync()
	{
		var content = await JS.InvokeAsync<string>("EditorMDE.getEditorValue");
		return content;
	}

}
