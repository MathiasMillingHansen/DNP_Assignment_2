namespace Shared.DTOs.User;

public class GetUserDto
{
    public GetUserDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }

    public string Password { get; set; }
}