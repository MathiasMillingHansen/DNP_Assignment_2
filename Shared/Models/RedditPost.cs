namespace Shared.Models;

public class RedditPost
{
    public RedditPost(string Title, string Body, User User)
    {
        this.Title = Title;
        this.Body = Body;
        UserName = User.Username;
    }

    public RedditPost()
    {
    }

    public string Title { get; set; }
    public string Body { get; set; }

    public User User { get; }

    public int Id { get; set; }
    public string UserName { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}, Body: {Body}, User: {User}";
    }
}