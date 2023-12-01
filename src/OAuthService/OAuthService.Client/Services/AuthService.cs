using System.Net.Http.Json;

namespace OAuthService.Client.Services;

public class AuthService(HttpClient client)
{
	public async Task<bool> RegisterAsync(string email, string password)
	{
		var response = await client.PostAsJsonAsync("/register", new { username = email , password });
		return response.IsSuccessStatusCode;
	}

	public async Task<bool> LoginAsync(string email, string password)
	{
		var res = await client.PostAsJsonAsync("/login?useCookies=true", new { username = email, password });
		return res.IsSuccessStatusCode;
	}
}
