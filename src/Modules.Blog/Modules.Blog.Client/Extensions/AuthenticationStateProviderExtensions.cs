using Microsoft.AspNetCore.Components.Authorization;

namespace Modules.Blog.Client.Extensions;

public static class AuthenticationStateProviderExtensions
{
	public static async Task<string?> GetUserNameAsync(this AuthenticationStateProvider authenticationStateProvider)
	{
		var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		return user.Identity?.Name;
	}
}
