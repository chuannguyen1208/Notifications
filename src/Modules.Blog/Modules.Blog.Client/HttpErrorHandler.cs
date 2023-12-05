
using Modules.Blog.Client.Services.Interop;

namespace Modules.Blog.Client;

public class HttpErrorHandler(CommonInterop commonInterop) : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var response = await base.SendAsync(request, cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			string content = (int)response.StatusCode switch
			{
				401 => "Unauthorized",
				403 => "Forbidden",
				500 => "Internal server error",
				_ => await response.Content.ReadAsStringAsync(cancellationToken)
			};

			await commonInterop.ToastError(content);
			throw new HttpRequestException(content);
		}

		return response;
	}
}