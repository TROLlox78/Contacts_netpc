using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Blazor.Services;

public class AuthHandler
{
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly IAuthService authService;

    public AuthHandler(AuthenticationStateProvider AuthenticationStateProvider, IAuthService authService)
    {
        authenticationStateProvider = AuthenticationStateProvider;
        this.authService = authService;
    }
    public async Task HandleLogin(UserDTO request)
    {
        var response = await authService.GetToken(request);
        if (response == null || !response.IsSuccessStatusCode) { return; }

        var token = await response.Content.ReadAsStringAsync();
        authService.StoreToken(token);

        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Name, request.Username),
        ], "Custom Authentication");

        var user = new ClaimsPrincipal(identity);

        ((AuthStateProvider)authenticationStateProvider)
            .AuthenticateUser(user);
    }
    public async Task HandleRegister(UserDTO request)
    {
        await authService.Register(request);
    }
}
