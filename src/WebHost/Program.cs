using Serilog;
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

app.UseSwaggerTool();

app.UseErrorHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.MapAuth();

app.Run();
