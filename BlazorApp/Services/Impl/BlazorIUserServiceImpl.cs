using System.Text.Json;
using BlazorApp.LoginModels;
using Shared.Models;

namespace BlazorApp.Services.Impl;

public class BlazorIUserServiceImpl : BlazorIUserService
{
    private readonly HttpClient _client;

    public BlazorIUserServiceImpl(HttpClient client)
    {
        _client = client;
    }

    public async Task<LoginUser> GetUserAsync(string username)
    {
        var response = await _client.GetAsync($"User/{username}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
        var user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return new LoginUser(user.Username, user.Password);
    }
}