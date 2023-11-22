using Microsoft.Extensions.DependencyInjection;

namespace Tools.Auth;
public static class AuthInstaller
{
	public static IServiceCollection AddAuth(this IServiceCollection services)
	{
		return services;
	}
}
