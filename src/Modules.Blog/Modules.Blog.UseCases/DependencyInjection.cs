using Microsoft.Extensions.DependencyInjection;

namespace Modules.Blog.UseCases;
public static class DependencyInjection
{
	public static IServiceCollection AddBlogsUseCases(this IServiceCollection services)
	{
		services.AddSingleton<IBlogsRepo, BlogsRepoSample>();
		return services;
	}
}
