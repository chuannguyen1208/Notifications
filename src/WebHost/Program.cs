using Serilog;
using Tools.ErrorHandling;
using Tools.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
	.AddSerilogLogging(builder.Configuration);

builder.Services.AddAuthentication();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseErrorHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();

