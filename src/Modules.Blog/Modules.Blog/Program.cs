using Modules.Blog.Components;
using System.Reflection;
using Tools.Swagger;
using Tools.MediatR;
using Modules.Blog.UseCases.Blogs.Queries;
using Tools.Routing;
using Modules.Blog.UseCases.Blogs;
using Modules.Blog.Client.Services;
using Microsoft.AspNetCore.Identity;
using Modules.Shared;
using Modules.Blog.Client.Services.Interop;
using Modules.Blog.Infra;
using Modules.Blog.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services
	.AddSwaggerTool()
	.AddMediatRTool(Assembly.GetExecutingAssembly(), typeof(GetBlogsQuery).Assembly);

builder.Services.AddHttpClient<BlogsService>(client => client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!))
	.AddHttpMessageHandler<HttpErrorHandler>();

builder.Services.AddTransient<HttpErrorHandler>();
builder.Services.AddScoped<EditorInterop>();
builder.Services.AddScoped<CommonInterop>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddScheme<ReverseProxyAuthenticationOptions, ReverseProxyAuthenticationHandler>(IdentityConstants.ApplicationScheme, o => { });

builder.Services.AddModuleBlogs();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

app.UsePathBase("/b");

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

app.MigrateModuleBlogs();

app.UseEndpoints<BlogEndpoints>();

app.Run();
