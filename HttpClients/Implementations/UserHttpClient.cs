using System.Net.Http.Json;
using System.Text.Json;
using Clients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Clients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<User> CreateUserAsync(UserCreationDto userToCreate)
    {
        var response = await _client.PostAsJsonAsync("User", userToCreate);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null)
    {
        var uri = "/user";
        if (!string.IsNullOrEmpty(usernameContains)) uri += $"?username={usernameContains}";
        var response = await _client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}