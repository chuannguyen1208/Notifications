using MediatR;
using Modules.Blog.Shared;
using Modules.Blog.Shared.Services;
using Modules.Blog.UseCases.Blogs.Commands;
using Modules.Blog.UseCases.Blogs.Queries;

namespace Modules.Blog.Services;

internal class BlogsService(IMediator mediator) : IBlogsService
{
	public async Task DeleteAsync(int id)
	{
		await mediator.Send(new DeleteBlogCommand(id)).ConfigureAwait(false);
	}

	public async Task EditBlogAsync(EditBlogDto editBlogDto)
	{
		await mediator.Send(new EditBlogCommand(
			Id: editBlogDto.Id,
			Title: editBlogDto.Title,
			Description: editBlogDto.Description,
			Content: editBlogDto.Content)).ConfigureAwait(false);
	}

	public async Task<BlogDto> GetBlogAsync(int id)
	{
		var res = await mediator.Send(new GetBlogQuery(id)).ConfigureAwait(false);
		return res;
	}

	public async Task<IEnumerable<BlogDto>> GetBlogsAsync()
	{
		var res = await mediator.Send(new GetBlogsQuery()).ConfigureAwait(false);
		return res;
	}

	public async Task<DashboardSummary> GetDashboardSummaryAsync()
	{
		var res = await mediator.Send(new GetDashboardSummaryQuery()).ConfigureAwait(false);
		return res;
	}
}
