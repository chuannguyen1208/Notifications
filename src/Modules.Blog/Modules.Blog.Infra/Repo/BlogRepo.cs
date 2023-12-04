using Modules.Blog.UseCases;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.Infra.Repo;

internal class BlogRepo : IBlogsRepo
{
	public Task CreateBlog(BlogEntity blog)
	{
		throw new NotImplementedException();
	}

	public Task<BlogEntity?> GetBlogAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<BlogEntity>> GetBlogs()
	{
		throw new NotImplementedException();
	}
}
