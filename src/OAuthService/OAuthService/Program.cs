using Microsoft.AspNetCore.Identity;
using OAuthService;
using OAuthService.Client.Pages;
using OAuthService.Client.Services;
using OAuthService.Components;
using System.Text.Json;
using Tools.Auth;
using Tools.Swagger;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddAuthTool()
	.AddSwaggerTool();

builder.Services.AddHttpClient<AuthService>(client => client.BaseAddress = new Uri("http://localhost:5002"));


builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
	.AddTransforms(tbc =>
	{
		if (!string.IsNullOrEmpty(tbc.Route.AuthorizationPolicy))
		{
			tbc.AddRequestTransform(rtc =>
			{
				var userDic = rtc.HttpContext.User.Claims.Aggregate(
					new Dictionary<string, string>(),
					(d, c) =>
					{
						d[c.Type] = c.Value;
						return d;
					}
				);

				rtc.ProxyRequest.Headers.Add("x-user-json", JsonSerializer.Serialize(userDic));
				return ValueTask.CompletedTask;
			});
		}
	});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddCookie(IdentityConstants.ApplicationScheme, o =>
	{
		o.LoginPath = "/login";
	});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
	app.UseSwaggerTool();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(Login).Assembly);

app.MigrateAuthTool();
app.MapAuthEndpoints();

app.MapReverseProxy();

await AuthDbSeeder.SeedData(app.Services);

app.Run();
