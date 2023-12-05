using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OAuthService.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("default", client =>
{
	client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
