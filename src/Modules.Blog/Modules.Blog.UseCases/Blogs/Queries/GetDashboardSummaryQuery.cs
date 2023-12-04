using MediatR;
using Modules.Blog.Shared;

namespace Modules.Blog.UseCases.Blogs.Queries;
public class GetDashboardSummaryQuery : IRequest<DashboardSummary>
{
	public class GetDashboardSummaryQueryHandler(IBlogsRepo blogsRepo) : IRequestHandler<GetDashboardSummaryQuery, DashboardSummary>
	{
		public async Task<DashboardSummary> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
		{
			var blogs = await blogsRepo.GetBlogs();
			var blogsCount = blogs.Count();

			return new DashboardSummary
			{
				TotalPosts = blogsCount,
			};
		}
	}
}
