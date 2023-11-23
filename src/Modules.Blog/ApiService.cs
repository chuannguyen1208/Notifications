using Microsoft.AspNetCore.Components;

namespace Modules.Blog;

internal class ApiService
{
	public ApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
	{
		HttpClient = httpClientFactory.CreateClient();
		HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
	}

	public HttpClient HttpClient { get; }
}