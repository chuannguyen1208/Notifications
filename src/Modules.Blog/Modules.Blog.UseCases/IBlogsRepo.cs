using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.UseCases;
public interface IBlogsRepo
{
	Task<IEnumerable<BlogEntity>> GetBlogs();
	Task CreateBlog(BlogEntity blog);
	Task<BlogEntity?> GetBlogAsync(int id);
}

internal class BlogsRepoSample : IBlogsRepo
{
	private readonly List<BlogEntity> _blogEntities;

	public BlogsRepoSample()
	{
		_blogEntities =
		[
			new BlogEntity
			{
				Id = 1,
				Title = "Title 1",
				Description = "Description",
				Created = DateTimeOffset.Parse("2022-01-01"),
				Published = true
			},
			new BlogEntity
			{
				Id = 2,
				Title = "Title 2",
				Description = "Description",
				Created = DateTimeOffset.Parse("2022-01-02")
			},
		];
	}

	public async Task CreateBlog(BlogEntity blog)
	{
		var maxId = _blogEntities.Select(s => s.Id).Max();
		blog.Id = maxId + 1;
		blog.Created = DateTimeOffset.UtcNow;
		_blogEntities.Add(blog);
		await Task.CompletedTask;
	}

	public async Task<BlogEntity?> GetBlogAsync(int id)
	{
		var blog = _blogEntities.Find(s => s.Id == id);
		return await Task.FromResult(blog);
	}

	public async Task<IEnumerable<BlogEntity>> GetBlogs()
	{
		return await Task.FromResult(_blogEntities);
	}
}
