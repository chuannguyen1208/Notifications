using MediatR;

namespace Modules.Blog.UseCases.Blogs.Commands;
public record DeleteBlogCommand(int Id) : IRequest
{
	public class DeleteBlogCommandHandler(IBlogsRepo blogsRepo) : IRequestHandler<DeleteBlogCommand>
	{
		public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
		{
			await blogsRepo.DeleteAsync(request.Id, cancellationToken);
		}
	}
}
