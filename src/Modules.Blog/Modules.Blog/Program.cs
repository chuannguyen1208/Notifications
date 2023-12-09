using Modules.Blog.Components;
using System.Reflection;
using Tools.Swagger;
using Tools.MediatR;
using Modules.Blog.UseCases.Blogs.Queries;
using Tools.Routing;
using Modules.Blog.UseCases.Blogs;
using Microsoft.AspNetCore.Identity;
using Modules.Shared;
using Modules.Blog.Client.Services.Interop;
using Modules.Blog.Infra;
using Modules.Blog.Client.Layout;
using Modules.Blog.Shared.Services;
using Modules.Blog.Services;
using Modules.Blog.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services
	.AddSwaggerTool()
	.AddMediatRTool(Assembly.GetExecutingAssembly(), typeof(GetBlogsQuery).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddScheme<ReverseProxyAuthenticationOptions, ReverseProxyAuthenticationHandler>(IdentityConstants.ApplicationScheme, o => { });

builder.Services.AddModuleBlogs();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpClient("default", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!);
});

builder.Services.AddScoped<IBlogsService, BlogsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IToastService, CommonInterop>();
builder.Services.AddTransient<IBlobService, BlobService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<EditorInterop>();

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
	.AddAdditionalAssemblies(typeof(BlogsLayout).Assembly);

app.MigrateModuleBlogs();

app.UseEndpoints<BlogEndpoints>();

app.Run();
