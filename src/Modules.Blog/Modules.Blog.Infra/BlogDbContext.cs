using Microsoft.EntityFrameworkCore;
using Modules.Blog.UseCases.Entity;

namespace Modules.Blog.Infra;
internal class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
	public DbSet<BlogEntity> Blogs { get; set; }

	public override int SaveChanges()
	{
		SetTimestamps();
		return base.SaveChanges();
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		SetTimestamps();
		return await base.SaveChangesAsync(cancellationToken);
	}

	private void SetTimestamps()
	{
		var entities = ChangeTracker.Entries()
			.Where(x => x.Entity is ICreatedModified && (x.State == EntityState.Added || x.State == EntityState.Modified));

		foreach (var entityEntry in entities)
		{
			var now = DateTime.UtcNow;

			if (entityEntry.State == EntityState.Added)
			{
				((ICreatedModified)entityEntry.Entity).Created = now;
			}

			((ICreatedModified)entityEntry.Entity).Modified = now;
		}
	}
}
