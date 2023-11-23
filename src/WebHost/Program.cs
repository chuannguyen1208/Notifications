using Serilog;
using System.Security.Claims;
using Tools.Auth;
using Tools.ErrorHandling;
using Tools.Logging;
using Tools.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
	.AddSwaggerTool()
	.AddSerilogLogging(builder.Configuration)
	.AddAuth();

builder.Host.UseSerilog();

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

app.MapAuth();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Ok").WithOpenApi();
app.MapGet("/user", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}").RequireAuthorization().WithOpenApi();

app.MapReverseProxy();

app.Run();

