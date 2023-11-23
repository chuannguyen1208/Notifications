using MediatR;

namespace Modules.Blog.UseCases.Blogs;

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
					Name = "Notification",
					Description = "Notification with Messaging"
				}
			});
		}
	}
}
