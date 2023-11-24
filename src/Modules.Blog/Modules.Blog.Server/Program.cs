using Tools.Swagger;
using Tools.ErrorHandling;
using Tools.MediatR;
using Tools.Routing;
using Modules.Blog.UseCases.Blogs;
using Modules.Blog.UseCases.Blogs.Queries;
using static Modules.Blog.Client.Pages.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddSwaggerTool()
	.AddMediatRTool(typeof(GetBlogsQuery).Assembly);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerTool();
	app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapGet("/api/weathers", () => new List<WeatherForecast>
{
	new()
	{
		Date = DateOnly.Parse("2020-01-01"),
		Summary = "",
		TemperatureC = 32,
	},
	new()
	{
		Date = DateOnly.Parse("2020-01-02"),
		Summary = "",
		TemperatureC = 10,
	}
});

app.UseEndpoints<BlogEndpoints>();

app.Run();
