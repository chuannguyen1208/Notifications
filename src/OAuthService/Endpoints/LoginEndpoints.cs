using Microsoft.AspNetCore.Authentication;
using OAuthService.Endpoints.OAuth;
using System.Security.Claims;
using System.Web;
using Tools.Routing;

namespace OAuthService.Endpoints;

public class LoginEndpoints : IEndpointsDefinition
{
	private const string LoginRoute = "/login";
	private const string OAuthRoute = "/oauth";

	public static void ConfigureEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet(LoginRoute, GetLogin);
		app.MapPost(LoginRoute, PostLogin);

		app.MapGet($"{OAuthRoute}/authorize", AuthorizationEndpoints.Handle).RequireAuthorization();
		app.MapPost($"{OAuthRoute}/token", TokenEndpoint.Handle);
	}

	private static async Task<IResult> PostLogin(
		HttpContext context,
		string returnUrl)
	{
		var claimsPrinciple = new ClaimsPrincipal(
				new ClaimsIdentity(
					new Claim[]
					{
						new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
					},
					"cookie"
				)
			);

		await context.SignInAsync("cookie", claimsPrinciple);

		return Results.Redirect(returnUrl);
	}

	private static async Task GetLogin(string returnUrl, HttpResponse response)
	{
		response.Headers.ContentType = "text/html";
		await response.WriteAsync(
			$"""
			<html>
				<head>
					<title>Login</title>
				</head>
				<body>
					<form action="/login?returnUrl={HttpUtility.UrlEncode(returnUrl)}" method="post">
						<input value="Submit" type="submit">
					</form>
				</body>
			</html>
			"""
		);
	}
}
