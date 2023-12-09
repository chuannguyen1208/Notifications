
using Modules.Blog.Shared.Services;

namespace Modules.Blog.Client;

internal class HttpErrorHandler(IToastService toast) : DelegatingHandler
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

			await toast.ToastError(content);
			throw new HttpRequestException(content);
		}

		await toast.ToastSuccess("Successful");
		return response;
	}
}