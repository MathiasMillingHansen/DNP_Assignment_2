using System.Security.Claims;
using System.Text.Json;
using BlazorApp.LoginModels;
using BlazorApp.Services;
using Microsoft.JSInterop;
using Shared.Models;

namespace BlazorApp.Authentication;

public class AuthManagerImpl : IAuthManager
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!; // assigning to null! to suppress null warning.
    private readonly BlazorIUserService _blazorIUserService;
    private readonly IJSRuntime jsRuntime;

    public AuthManagerImpl(BlazorIUserService blazorIUserService, IJSRuntime jsRuntime)
    {
        
        _blazorIUserService = blazorIUserService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password)
    {
        LoginUser? loginUser = await _blazorIUserService.GetUserAsync(username); // Get user from database

        ValidateLoginCredentials(password, loginUser); // Validate input data against data from database
        // validation success
        await CacheUserAsync(loginUser!); // Cache the user object in the browser 

        ClaimsPrincipal principal = CreateClaimsPrincipal(loginUser); // convert user object to ClaimsPrincipal

        OnAuthStateChanged?.Invoke(principal); // notify interested classes in the change of authentication state
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync(); // remove the user object from browser cache
        ClaimsPrincipal principal = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
        OnAuthStateChanged?.Invoke(principal); // notify about change in authentication state
    }

    public async Task<ClaimsPrincipal> GetAuthAsync() // this method is called by the authentication framework, whenever user credentials are reguired
    {
        LoginUser? loginUser =  await GetUserFromCacheAsync(); // retrieve cached user, if any

        ClaimsPrincipal principal = CreateClaimsPrincipal(loginUser); // create ClaimsPrincipal

        return principal;
    }

    private async Task<LoginUser?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        LoginUser loginUser = JsonSerializer.Deserialize<LoginUser>(userAsJson)!;
        return loginUser;
    }

    private static void ValidateLoginCredentials(string password, LoginUser? loginUser)
    {
        if (loginUser == null)
        {
            throw new Exception("Username not found");
        }

        if (!string.Equals(password,loginUser.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(LoginUser? loginUser)
    {
        if (loginUser != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(loginUser);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }

    private async Task CacheUserAsync(LoginUser loginUser)
    {
        string serialisedData = JsonSerializer.Serialize(loginUser);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private static ClaimsIdentity ConvertUserToClaimsIdentity(LoginUser loginUser)
    {
        // here we take the information of the User object and convert to Claims
        // this is (probably) the only method, which needs modifying for your own user type
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, loginUser.UserName),
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
}