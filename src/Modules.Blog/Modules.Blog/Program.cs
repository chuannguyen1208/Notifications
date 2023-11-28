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

var builder = WebApplication.CreateBuilder(args);

var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl") ?? "http://localhost:5002";
var callBackUrl = builder.Configuration.GetValue<string>("CallBackUrl") ?? "http://localhost:5002";

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddSwaggerTool()
	.AddMediatRTool(Assembly.GetExecutingAssembly(), typeof(GetBlogsQuery).Assembly);

builder.Services.AddBlogsUseCases();
builder.Services.AddHttpClient<BlogsService>(client => client.BaseAddress = new Uri("http://localhost:5003"));

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
	options.SignInScheme = IdentityConstants.ApplicationScheme;
	options.Authority = identityUrl.ToString();
	options.SignedOutRedirectUri = callBackUrl.ToString();
	options.RequireHttpsMetadata = false;
});

var app = builder.Build();

app.UsePathBase("/blogs");

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
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(BlogsService).Assembly);

app.UseEndpoints<BlogEndpoints>();

app.Run();
