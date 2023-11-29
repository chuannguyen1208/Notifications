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

		services.AddAuthentication(IdentityConstants.ApplicationScheme)
			.AddCookie(IdentityConstants.ApplicationScheme, o =>
			{
				o.LoginPath = "/login";
			});

		services.AddAuthorization();

		return services;
	}

	public static IEndpointRouteBuilder MapAuthTool(this IEndpointRouteBuilder app)
	{
		app.MapIdentityApi<IdentityUser>();
		return app;
	}
}
