using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using UserManagement.Client;
using UserManagement.Client.Services.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(builder =>
{
    builder.AddPolicy("NotAutentiticationUser", pb =>
    {
        pb.RequireAssertion(handler => !handler.User.Identity!.IsAuthenticated);
    });
});
builder.Services.AddScoped<HostAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<HostAuthenticationStateProvider>());

await builder.Build().RunAsync();
