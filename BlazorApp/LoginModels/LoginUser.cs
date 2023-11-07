namespace BlazorApp.LoginModels;

public class LoginUser
{
    public LoginUser(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}