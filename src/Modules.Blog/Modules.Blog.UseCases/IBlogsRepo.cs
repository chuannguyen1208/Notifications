using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.UseCases;

public interface IBlogsRepo
{
	Task<IEnumerable<BlogEntity>> GetBlogs();
	Task<BlogEntity?> GetBlogAsync(int id);
	Task CreateBlog(BlogEntity blog);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task EditBlog(BlogEntity blog);
}
