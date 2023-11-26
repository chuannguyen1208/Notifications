using Modules.Blog.Shared;
using System.Net.Http.Json;

namespace Modules.Blog.Client.Services;

public class BlogsService(HttpClient httpClient)
{
	private const string BaseUrl = "api/blogs";
	public async Task<IEnumerable<BlogDto>> GetBlogsAsync()
	{
		return await httpClient.GetFromJsonAsync<IEnumerable<BlogDto>>(BaseUrl) ?? Enumerable.Empty<BlogDto>();
	}

	public async Task EditBlogAsync(EditBlogDto editBlogDto)
	{
		await httpClient.PostAsJsonAsync(BaseUrl, editBlogDto);
	}
}
