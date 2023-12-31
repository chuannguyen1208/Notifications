﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Modules.Shared;

public class ReverseProxyAuthenticationOptions : AuthenticationSchemeOptions
{
	public string AuthHeaderName { get; set; } = "x-user-json";
}

public class ReverseProxyAuthenticationHandler(
	IOptionsMonitor<ReverseProxyAuthenticationOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder) : AuthenticationHandler<ReverseProxyAuthenticationOptions>(options, logger, encoder)
{
	private static readonly string[] _defaultHeader = ["{}"];

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync() =>
		await Task.Run(() =>
		{
			var authenticationHeader = Request.Headers[Options.AuthHeaderName];

			if (string.IsNullOrEmpty(authenticationHeader) || authenticationHeader.Equals(_defaultHeader))
			{
				return AuthenticateResult.Fail("Unauthenticated.");
			}

			var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(authenticationHeader.ToString()) ?? [];

			var userClaims = dictionary.Select(v => new Claim(v.Key, v.Value));

			var claimsIdentity = new ClaimsIdentity(userClaims, Scheme.Name);

			var claimPrinciple = new ClaimsPrincipal(claimsIdentity);

			return AuthenticateResult.Success(new AuthenticationTicket(claimPrinciple, Scheme.Name));
		});
}
