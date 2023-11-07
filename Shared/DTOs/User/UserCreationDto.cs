namespace Shared.DTOs;

public class UserCreationDto
{
    public UserCreationDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; }
    public string Password { get; }
}