using OAuthService.Shared;
using System.Net.Http.Json;

namespace OAuthService.Client.Services;

public interface IAuthService
{
	Task<bool> RegisterAsync(RegisterModel model);
	Task<bool> LoginAsync(LoginModel model);
	Task LogoutAsync();
}

internal class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
	public async Task<bool> LoginAsync(LoginModel model)
	{
		using var client = httpClientFactory.CreateClient("default");
		var res = await client.PostAsJsonAsync("/login", model);
		return res.IsSuccessStatusCode;
	}

	public async Task LogoutAsync()
	{
		using var client = httpClientFactory.CreateClient("default");
		await client.PostAsync("/logout", null);
	}

	public async Task<bool> RegisterAsync(RegisterModel model)
	{
		using var client = httpClientFactory.CreateClient("default");
		var response = await client.PostAsJsonAsync("/register", model);
		return response.IsSuccessStatusCode;
	}
}
