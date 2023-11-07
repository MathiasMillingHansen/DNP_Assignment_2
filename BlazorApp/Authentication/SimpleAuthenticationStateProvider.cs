using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Authentication;

public class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthManager authManager;

    public SimpleAuthenticationStateProvider(IAuthManager authManager)
    {
        this.authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = await authManager.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}