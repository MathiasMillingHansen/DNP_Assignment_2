namespace Shared.DTOs.User;

public class GetUserWithPasswordDto
{
    public GetUserWithPasswordDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}