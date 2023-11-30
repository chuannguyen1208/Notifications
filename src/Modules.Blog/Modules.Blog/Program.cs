using Modules.Blog.Components;
using System.Reflection;
using Tools.Swagger;
using Tools.MediatR;
using Modules.Blog.UseCases.Blogs.Queries;
using Tools.Routing;
using Modules.Blog.UseCases.Blogs;
using Modules.Blog.Client.Services;
using Modules.Blog.UseCases;
using Microsoft.AspNetCore.Identity;
using Modules.Blog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services
	.AddSwaggerTool()
	.AddMediatRTool(Assembly.GetExecutingAssembly(), typeof(GetBlogsQuery).Assembly);

builder.Services.AddBlogsUseCases();
builder.Services.AddHttpClient<BlogsService>(client => client.BaseAddress = new Uri("http://localhost:5001"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddScheme<ReverseProxyAuthenticationOptions, ReverseProxyAuthenticationHandler>(IdentityConstants.ApplicationScheme, o => { });

var app = builder.Build();

app.UsePathBase("/blogs");

app.UseAuthentication();
app.UseAuthorization();

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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(BlogsService).Assembly);

app.UseEndpoints<BlogEndpoints>();

app.Run();
