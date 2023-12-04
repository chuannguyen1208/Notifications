using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Blog.Infra.Repo;
using Modules.Blog.UseCases;

namespace Modules.Blog.Infra;

public static class DependencyInjection
{
	public static IServiceCollection AddModuleBlogs(this IServiceCollection services)
	{
		services.AddDbContext<BlogDbContext>(o => o.UseSqlite("DataSource=./db/blog.db"));
		services.AddScoped<IBlogsRepo, BlogRepo>();
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
