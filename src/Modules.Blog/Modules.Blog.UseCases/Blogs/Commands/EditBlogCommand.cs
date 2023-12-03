using AutoMapper;
using MediatR;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.UseCases.Blogs.Commands;

public record EditBlogCommand(int Id, string Title, string Description, string Content) : IRequest
{
	public class EditBlogCommandProfile : Profile
	{
		public EditBlogCommandProfile()
		{
			CreateMap<EditBlogCommand, BlogEntity>();
		}
	}

	public class EditBlogCommandHandler(IMapper mapper, IBlogsRepo blogsRepo) : IRequestHandler<EditBlogCommand>
	{
		public async Task Handle(EditBlogCommand request, CancellationToken cancellationToken)
		{
			var blog = mapper.Map<BlogEntity>(request);

			if (blog.Id == 0)
			{
				await blogsRepo.CreateBlog(blog);
			}
		}
	}
}