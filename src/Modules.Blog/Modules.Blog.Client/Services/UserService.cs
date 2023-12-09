using Microsoft.AspNetCore.Components.Authorization;
using Modules.Blog.Shared.Services;
using System.Security.Claims;

namespace Modules.Blog.Client.Services;

public class UserService(AuthenticationStateProvider authenticationStateProvider) : IUserService
{
	public async Task<string?> CurrentUserName()
	{
		var user = await GetPrinciple().ConfigureAwait(false);
		return user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
	}

	public async Task<bool> IsAuthenticated()
	{
		var user = await GetPrinciple().ConfigureAwait(false);
		return user?.Identity?.IsAuthenticated ?? false;
	}

	private async Task<ClaimsPrincipal?> GetPrinciple()
	{
		var authState = await authenticationStateProvider.GetAuthenticationStateAsync().ConfigureAwait(false);
		var user = authState.User;
		return user;
	}
}
