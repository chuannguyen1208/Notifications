
namespace Modules.Blog.Client;

public class HttpErrorHandler : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var response = await base.SendAsync(request, cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadAsStringAsync(cancellationToken);
			Console.WriteLine($"Request is not successful with error: " + content);
			throw new HttpRequestException(content); 
		}

		return response;
	}
}