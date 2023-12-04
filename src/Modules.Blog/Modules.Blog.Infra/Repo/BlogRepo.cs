using Modules.Blog.UseCases;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.Infra.Repo;

internal class BlogRepo(BlogDbContext context) : IBlogsRepo
{
	public async Task CreateBlog(BlogEntity blog)
	{
		context.Add(blog);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var entity = context.Blogs.Find(id);
		if (entity != null)
		{
			context.Blogs.Remove(entity);
			await context.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task<BlogEntity?> GetBlogAsync(int id)
	{
		var entity = context.Blogs.Find(id);
		return await Task.FromResult(entity);
	}

	public async Task<IEnumerable<BlogEntity>> GetBlogs()
	{
		var entities = context.Blogs.AsEnumerable();
		return await Task.FromResult(entities);
	}
}
