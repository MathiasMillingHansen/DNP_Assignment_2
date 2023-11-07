using System.Text.Json;
using BlazorApp.LoginModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BlazorApp.Services.Impl;

public class BlazorIUserServiceImpl : BlazorIUserService
{
    HttpClient _client;

    public BlazorIUserServiceImpl(HttpClient client)
    {
        _client = client;
    }

    public async Task<LoginUser> GetUserAsync(string username)
    {
        HttpResponseMessage response = await _client.GetAsync($"User/{username}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return new LoginUser(user.Username, user.Password);
        
    }
}