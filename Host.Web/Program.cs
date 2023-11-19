using Serilog;
using Tools.ErrorHandling;
using Tools.Logging;
using Tools.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSerilogLogging(builder.Configuration)
    .AddAsyncProcessing(builder.Configuration, assembliesWithConsumers: []);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

Log.Information("Start application!");

app
    .UseErrorHandling()
    .ApplyOutboxMigrations();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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

app.Run();
