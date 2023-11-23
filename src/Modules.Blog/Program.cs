using Modules.Blog.Components;
using Modules.Blog.Endpoints;
using Tools.Swagger;
using Tools.Routing;
using Tools.MediatR;
using System.Reflection;
using Modules.Blog.UseCases.Blogs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSwaggerTool()
	.AddMediatRTool(Assembly.GetExecutingAssembly(), typeof(GetBlogsQuery).Assembly);

builder.Services.AddHttpClient();
builder.Services.AddTransient<Modules.Blog.ApiService>();

var app = builder.Build();

app.UsePathBase("/blog");

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
    .AddInteractiveWebAssemblyRenderMode();

app.UseEndpoints<BlogEndpoints>();

app.Run();
