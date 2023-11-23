using MediatR;
using Modules.Blog.Shared;

namespace Modules.Blog.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<IEnumerable<BlogDto>>
{
	public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, IEnumerable<BlogDto>>
	{
		public async Task<IEnumerable<BlogDto>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
		{
			return await Task.FromResult(new List<BlogDto>
			{
				new()
				{
					Id = 1,
					Title = "Notification",
					Description = "Notification with Messaging."
				},
				new()
				{
					Id= 2,
					Title = ".NET Aspire",
					Description = ".NET Aspire simplify cloud and services dependency."
				}
			});
		}
	}
}
