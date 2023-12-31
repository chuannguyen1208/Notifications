﻿using Modules.Blog.Shared;
using Modules.Blog.Shared.Services;
using System.Net.Http.Json;

namespace Modules.Blog.Client.Services;

internal class BlogsService(IHttpClientFactory httpClientFactory) : IBlogsService
{
	private const string BaseUrl = "api/blogs";

	public async Task<IEnumerable<BlogDto>> GetBlogsAsync()
	{
		using var httpClient = httpClientFactory.CreateClient("default");
		return await httpClient.GetFromJsonAsync<IEnumerable<BlogDto>>(BaseUrl).ConfigureAwait(false) ?? Enumerable.Empty<BlogDto>();
	}

	public async Task<BlogDto> GetBlogAsync(int id)
	{
		using var httpClient = httpClientFactory.CreateClient("default");
		return await httpClient.GetFromJsonAsync<BlogDto>($"{BaseUrl}/{id}").ConfigureAwait(false) ?? throw new KeyNotFoundException();
	}

	public async Task<DashboardSummary> GetDashboardSummaryAsync()
	{
		using var httpClient = httpClientFactory.CreateClient("default");
		return await httpClient.GetFromJsonAsync<DashboardSummary>($"{BaseUrl}/summary").ConfigureAwait(false) ?? new DashboardSummary();
	}

	public async Task EditBlogAsync(EditBlogDto editBlogDto)
	{
		using var httpClient = httpClientFactory.CreateClient("default");
		await httpClient.PostAsJsonAsync(BaseUrl, editBlogDto).ConfigureAwait(false);
	}

	public async Task DeleteAsync(int id)
	{
		using var httpClient = httpClientFactory.CreateClient("default");
		await httpClient.DeleteAsync($"{BaseUrl}/{id}").ConfigureAwait(false);
	}
}
