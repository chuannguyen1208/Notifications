using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Modules.Blog.Client;
using Modules.Blog.Client.Services;
using Modules.Blog.Client.Services.Interop;
using Modules.Blog.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("default", client =>
{
	client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<HttpErrorHandler>();

builder.Services.AddTransient<HttpErrorHandler>();
builder.Services.AddScoped<IBlogsService, BlogsService>();
builder.Services.AddTransient<IBlobService, CommonInterop>();
builder.Services.AddTransient<IToastService, CommonInterop>();
builder.Services.AddTransient<EditorInterop>();

await builder.Build().RunAsync();
