namespace Shared.DTOs.User;

public class GetUserDto
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public GetUserDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}