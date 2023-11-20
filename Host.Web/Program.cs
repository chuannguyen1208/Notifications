using Serilog;
using Tools.ErrorHandling;
using Tools.Logging;
using Tools.Messaging;
using Tools.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSerilogLogging()
    .AddAsyncProcessing(builder.Configuration, assembliesWithConsumers: [])
    .AddSwagger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

Log.Information("Start application!");

app
    .UseErrorHandling()
    .ApplyOutboxMigrations();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Ok";
})
.WithTags("Host")
.WithOpenApi();

app.MapGet("/exception", () =>
{
    throw new ArgumentException("Test exception handling");
})
.WithTags("Host")
.WithOpenApi();

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

    opts.RoutePrefix = string.Empty;
});

app.Run();
