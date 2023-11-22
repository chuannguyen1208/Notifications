using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using System.Security.Claims;
using Tools.ErrorHandling;
using Tools.Logging;
using Tools.Messaging;
using Tools.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
	.AddSwagger()
	.AddSerilogLogging(builder.Configuration);

builder.Host.UseSerilog();

builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie();

var app = builder.Build();

app.UseErrorHandling();

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
	var descriptions = app.DescribeApiVersions();
	foreach (var groupName in descriptions.Select(d => d.GroupName))
	{
		opts.SwaggerEndpoint(
			url: $"/swagger/{groupName}/swagger.json",
			name: groupName);
	}
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Ok").WithOpenApi();

app.MapGet("/login", () =>
{
	var claims = new List<Claim>
	{
		new("id", Guid.NewGuid().ToString()),
		new("name", "Admin")
	};

	var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
	var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
	var signIn = Results.SignIn(claimsPrinciple, authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme);

	return signIn;
});

app.MapGet("/logout", () => Results.SignOut());

app.MapReverseProxy();

app.Run();

