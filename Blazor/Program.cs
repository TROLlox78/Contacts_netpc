using Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7078/") });
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<AuthenticationStateProvider, AuthStateProvider>();
        builder.Services.AddScoped<AuthHandler>();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();

        await builder.Build().RunAsync();
    }
}
