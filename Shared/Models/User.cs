namespace Shared.Models;

public class User
{

    public string Username { get; set; }
    public string Password { get; set; }
    
    public List<RedditPost> RedditPosts { get; set; }

    public User(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
        RedditPosts = new List<RedditPost>();
    }

}