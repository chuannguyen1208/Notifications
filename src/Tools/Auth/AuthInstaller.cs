using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.Auth;
public static class AuthInstaller
{
	public static IServiceCollection AddAuthTool(this IServiceCollection services)
	{
		services.AddDbContext<AuthDbContext>(o => o.UseSqlite("DataSource=app.db"));

		services.AddIdentityCore<IdentityUser>()
			.AddEntityFrameworkStores<AuthDbContext>()
			.AddApiEndpoints();

		return services;
	}

	public static IEndpointRouteBuilder MapAuthTool(this IEndpointRouteBuilder app)
	{
		app.MapIdentityApi<IdentityUser>();
		return app;
	}

	public static void MigrateAuthTool(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
		dbContext.Database.Migrate();
	}
}
