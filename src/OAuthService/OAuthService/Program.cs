using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OAuthService;
using OAuthService.Client.Pages;
using OAuthService.Client.Services;
using OAuthService.Components;
using OAuthService.Services;
using OAuthService.Shared;
using System.Text.Json;
using Tools.Auth;
using Tools.Swagger;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddAuthTool()
	.AddSwaggerTool();

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
	.AddTransforms(tbc =>
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
	});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddCookie(IdentityConstants.ApplicationScheme, o =>
	{
		o.LoginPath = "/login";
	});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();

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
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(Login).Assembly);

app.MigrateAuthTool();
app.MapReverseProxy();

app.MapPost("/login", PostLogin);
app.MapPost("/register", PostRegister);

await AuthDbSeeder.SeedData(app.Services);
app.Run();

async Task<IResult> PostLogin([FromBody] LoginModel model, [FromServices] IAuthService authService) 
{
	var res = await authService.LoginAsync(model);
	return res switch
	{
		false => Results.BadRequest("Invalid username or password."),
		_ => Results.Ok()
	};
}


async Task<IResult> PostRegister([FromBody] RegisterModel model, [FromServices] IAuthService authService)
{
	var res = await authService.RegisterAsync(model);
	return res switch
	{
		false => Results.BadRequest(),
		_ => Results.Created()
	};
}
