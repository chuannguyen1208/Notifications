using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Blog.Shared;
using Modules.Blog.Shared.Services;
using Tools.Routing;

namespace Modules.Blog.UseCases.Blogs;
public class BlogEndpoints : IEndpointsDefinition
{
	public static void ConfigureEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/blogs", GetBlogs);
		app.MapGet("/api/blogs/{id}", GetBlogById);
		app.MapGet("/api/blogs/summary", GetDashboardSummary);
		app.MapPost("/api/blogs", EditBlog).RequireAuthorization();
		app.MapDelete("/api/blogs/{id}", DeleteBlog).RequireAuthorization();
		app.MapPost("/api/files", UploadFile).DisableAntiforgery().WithTags("Files");
	}

	private static async Task<IResult> UploadFile(IFormFile formFile, [FromServices] IWebHostEnvironment env)
	{
		if (formFile.Length > 0)
		{
			var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(formFile.FileName));
			var returnFilePath = Path.Combine("img\\temp", fileName);
			var filePath = Path.Combine(env.ContentRootPath, "wwwroot", returnFilePath);
			using var stream = File.Create(filePath);
			await formFile.CopyToAsync(stream);
			return Results.Ok(returnFilePath);
		}
		return Results.Empty;
	}

	private static async Task<IResult> DeleteBlog(int id, [FromServices] IBlogsService blogsService)
	{
		await blogsService.DeleteAsync(id);
		return Results.NoContent();
	}

	private static async Task<IResult> EditBlog([FromBody] EditBlogDto model, [FromServices] IBlogsService blogsService)
	{
		await blogsService.EditBlogAsync(model);
		return Results.Created();
	}

	private static async Task GetDashboardSummary([FromServices] IBlogsService blogsService) =>
		await blogsService.GetDashboardSummaryAsync();

	private static async Task GetBlogById(int id, [FromServices] IBlogsService blogsService) =>
		await blogsService.GetBlogAsync(id);

	private static async Task GetBlogs([FromServices] IBlogsService blogsService) => 
		await blogsService.GetBlogsAsync();
}
