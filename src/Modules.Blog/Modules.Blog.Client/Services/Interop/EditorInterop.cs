using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class EditorInterop
{
	private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

	public EditorInterop(IJSRuntime js)
	{
		_moduleTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>("import", "./assets/js/editor.js").AsTask());
	}

	public async ValueTask LoadEditorAsync(ElementReference textareaElement, string toolbar = "")
	{
		var module = await _moduleTask.Value;
		await module.InvokeVoidAsync("loadEditor", textareaElement, toolbar);
	}
}
