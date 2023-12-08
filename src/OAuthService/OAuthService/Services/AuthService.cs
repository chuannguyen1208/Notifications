using Microsoft.AspNetCore.Identity;
using OAuthService.Client.Services;
using OAuthService.Shared;

namespace OAuthService.Services;

/// <summary>
/// SignInManager and UserManager don't work on blazor mode server-side rendering, pls use WebAssembly.
/// </summary>
/// <param name="signInManager"></param>
/// <param name="userManager"></param>
internal class AuthService(
	SignInManager<IdentityUser> signInManager, 
	UserManager<IdentityUser> userManager) : IAuthService
{
	public async Task<bool> LoginAsync(LoginModel model)
	{
		var res = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, true, true);
		return res.Succeeded;
	}

	public async Task LogoutAsync()
	{
		await signInManager.SignOutAsync();
	}

	public async Task<bool> RegisterAsync(RegisterModel model)
	{
		var user = new IdentityUser(model.Email!);
		var res = await userManager.CreateAsync(user, model.Password!);

		return res.Succeeded;
	}
}
