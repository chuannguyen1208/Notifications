using MediatR;

namespace Modules.Blog.UseCases.Blogs.Commands;

public record EditBlogCommand(int Id, string Title, string Description) : IRequest
{
	public class EditBlogCommandHandler : IRequestHandler<EditBlogCommand>
	{
		public async Task Handle(EditBlogCommand request, CancellationToken cancellationToken)
		{
			await Task.CompletedTask;
		}
	}
}