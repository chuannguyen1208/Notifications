using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Blog.Infra.Repo;
using Modules.Blog.UseCases;
using Modules.Blog.UseCases.Blogs;

namespace Modules.Blog.Infra;

public static class DependencyInjection
{
	public static IServiceCollection AddModuleBlogs(this IServiceCollection services)
	{
		services.AddDbContext<BlogDbContext>(o => o.UseSqlite("DataSource=./db/blog.db"));
		services.AddScoped<IBlogsRepo, BlogRepo>();
		services.AddSingleton<MarkdigProvider>();
		return services;
	}

	public static IApplicationBuilder MigrateModuleBlogs(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
		dbContext.Database.Migrate();

		return app;
	}
}
