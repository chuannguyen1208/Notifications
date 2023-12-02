using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Modules.Blog.Client.Services.Interop;

public class EditorInterop(IJSRuntime js)
{
	public async ValueTask LoadEditorAsync(ElementReference textareaElement, string toolbar = "")
	{
		await js.InvokeVoidAsync("EditorMDE.loadEditor", textareaElement);
	}
}
