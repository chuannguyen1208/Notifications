using AutoMapper;
using MediatR;
using Modules.Blog.Shared;

namespace Modules.Blog.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<IEnumerable<BlogDto>>
{
	public class GetBlogsQueryHandler(IBlogsRepo blogsRepo, IMapper mapper) : IRequestHandler<GetBlogsQuery, IEnumerable<BlogDto>>
	{
		public async Task<IEnumerable<BlogDto>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
		{
			var res = await blogsRepo.GetBlogs();
			return mapper.Map<IEnumerable<BlogDto>>(res);
		}
	}
}
