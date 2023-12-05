using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Modules.Blog.Client;
using Modules.Blog.Client.Services;
using Modules.Blog.Client.Services.Interop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<BlogsService>(httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
	.AddHttpMessageHandler<HttpErrorHandler>();

builder.Services.AddTransient<HttpErrorHandler>();
builder.Services.AddScoped<EditorInterop>();
builder.Services.AddScoped<CommonInterop>();

await builder.Build().RunAsync();
