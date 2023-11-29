using Serilog;
using Tools.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication()
	.AddCookie();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.MapAuthTool();

app.Run();
