using MediatR;
using Modules.Blog.UseCases.Blogs;
using Tools.Routing;

namespace Modules.Blog.Endpoints;

public class BlogEndpoints : IEndpointsDefinition
{
	public static void ConfigureEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/blogs", (IMediator mediator) => mediator.Send(new GetBlogsQuery())).WithOpenApi();
	}
}
