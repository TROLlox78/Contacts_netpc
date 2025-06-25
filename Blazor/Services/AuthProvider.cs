using System.Security.Claims;
using Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTOs;

public class AuthStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }

    public async void AuthenticateUser(ClaimsPrincipal user)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(user)));
    }

}