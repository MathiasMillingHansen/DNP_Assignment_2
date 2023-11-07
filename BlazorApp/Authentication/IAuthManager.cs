using System.Security.Claims;

namespace BlazorApp.Authentication;

public interface IAuthManager
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
}