using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly ICollection<User> _users;

    public AuthService(HttpClient client)
    {
        _client = client;
        _users = loadUsers().Result;
    }

    public Task<User> ValidateUser(string username, string password)
    {
        var existingUser = _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(existingUser.Username + existingUser.Password);

        if (existingUser == null) throw new Exception("User not found");

        if (!existingUser.Password.Equals(password)) throw new Exception("Password mismatch");

        Console.WriteLine("User validated and returned");

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username)) throw new ValidationException("Username cannot be null");

        if (string.IsNullOrEmpty(user.Password)) throw new ValidationException("Password cannot be null");
        // Do more user info validation here

        // save to persistence instead of list

        _users.Add(user);

        return Task.CompletedTask;
    }

    private async Task<ICollection<User>> loadUsers()
    {
        var response = await _client.GetAsync("User");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var users = JsonSerializer.Deserialize<ICollection<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public Task<User> GetUser(string username, string password)
    {
        throw new NotImplementedException();
    }
}