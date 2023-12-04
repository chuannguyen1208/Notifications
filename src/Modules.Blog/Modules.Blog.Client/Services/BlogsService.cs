using Modules.Blog.Shared;
using System.Net.Http.Json;

namespace Modules.Blog.Client.Services;

public class BlogsService(HttpClient httpClient)
{
	private const string BaseUrl = "api/blogs";
	public async Task<IEnumerable<BlogDto>> GetBlogsAsync()
	{
		return await httpClient.GetFromJsonAsync<IEnumerable<BlogDto>>(BaseUrl).ConfigureAwait(false) ?? Enumerable.Empty<BlogDto>();
	}

	public async Task<BlogDto> GetBlogAsync(int id)
	{
		return await httpClient.GetFromJsonAsync<BlogDto>($"{BaseUrl}/{id}").ConfigureAwait(false) ?? throw new KeyNotFoundException();
	}

	public async Task<DashboardSummary> GetDashboardSummaryAsync()
	{
		return await httpClient.GetFromJsonAsync<DashboardSummary>($"{BaseUrl}/summary").ConfigureAwait(false) ?? new DashboardSummary();
	}

	public async Task EditBlogAsync(EditBlogDto editBlogDto)
	{
		await httpClient.PostAsJsonAsync(BaseUrl, editBlogDto).ConfigureAwait(false);
	}
}
