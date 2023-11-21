using Serilog;
using Tools.ErrorHandling;
using Tools.Logging;
using Tools.Messaging;
using Tools.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwagger()
    .AddSerilogLogging(builder.Configuration)
    .AddAsyncProcessing(builder.Configuration, []);

builder.Host.UseSerilog();

var app = builder.Build();

app.UseErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Ok").WithOpenApi();

app.Run();

