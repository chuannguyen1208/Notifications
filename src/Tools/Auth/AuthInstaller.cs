using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.Auth;
public static class AuthInstaller
{
	public static IServiceCollection AddAuth(this IServiceCollection services)
	{
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();

		services.AddAuthorizationBuilder();

		services.AddDbContext<AuthDbContext>(o => o.UseSqlite("DataSource=app.db"));

		services.AddIdentityCore<IdentityUser>()
			.AddEntityFrameworkStores<AuthDbContext>()
			.AddApiEndpoints();

		return services;
	}

	public static IEndpointRouteBuilder MapAuth(this IEndpointRouteBuilder app)
	{
		app.MapIdentityApi<IdentityUser>();
		return app;
	}
}
