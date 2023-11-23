using Microsoft.AspNetCore.Components;
using Modules.Blog.Shared;

namespace Modules.Blog;

public class ApiService : IApiService
{
	public ApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
	{
		HttpClient = httpClientFactory.CreateClient();
		HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
	}

	public HttpClient HttpClient { get; }
}
