using Microsoft.AspNetCore.Identity;

namespace OAuthService;

public static class AuthDbSeeder
{
	public static async Task SeedData(IServiceProvider sp)
	{
		using var scope = sp.CreateScope();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
		var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

		var user = new IdentityUserSeed();
		configuration.GetSection("IdentityUser").Bind(user);
		await userManager.CreateAsync(user, user.Password);
	}
}

internal class IdentityUserSeed : IdentityUser
{
	public string Password { get; set; } = null!;
}
