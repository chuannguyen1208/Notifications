using AutoMapper;
using MediatR;
using Modules.Blog.Shared;

namespace Modules.Blog.UseCases.Blogs.Queries;
public record GetBlogQuery(int Id) : IRequest<BlogDto>
{
	public class GetBlogQueryHandler(IBlogsRepo blogsRepo, IMapper mapper) : IRequestHandler<GetBlogQuery, BlogDto>
	{
		public async Task<BlogDto> Handle(GetBlogQuery request, CancellationToken cancellationToken)
		{
			var blog = await blogsRepo.GetBlogAsync(request.Id);
			var res = mapper.Map<BlogDto>(blog);

			return res;
		}
	}
}
