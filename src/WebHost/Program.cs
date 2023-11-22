using Serilog;
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

app.MapGet("/", () => "Ok").WithOpenApi();

app.MapReverseProxy();

app.Run();

