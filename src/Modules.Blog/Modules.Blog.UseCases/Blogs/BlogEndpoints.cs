using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Modules.Blog.UseCases.Blogs.Commands;
using Modules.Blog.UseCases.Blogs.Queries;
using Tools.Routing;

namespace Modules.Blog.UseCases.Blogs;
public class BlogEndpoints : IEndpointsDefinition
{
	public static void ConfigureEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/blogs", (IMediator mediator) => mediator.Send(new GetBlogsQuery())).WithOpenApi();
		app.MapPost("/api/blogs", (EditBlogCommand model, IMediator mediator) =>
		{
			return mediator.Send(model);
		});
	}
}
