using Microsoft.EntityFrameworkCore;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.Infra;
internal class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
	public DbSet<BlogEntity> Blogs { get; set; }
}
