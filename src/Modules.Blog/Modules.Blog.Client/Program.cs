using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Modules.Blog.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<BlogsService>(httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
