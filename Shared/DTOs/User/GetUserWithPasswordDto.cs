namespace Shared.DTOs.User;

public class GetUserWithPasswordDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    public GetUserWithPasswordDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}