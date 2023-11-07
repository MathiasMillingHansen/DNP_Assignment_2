using BlazorApp.LoginModels;

namespace BlazorApp.Services;

public interface BlazorIUserService
{
    public Task<LoginUser> GetUserAsync(string username);
}