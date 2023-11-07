using Shared.DTOs;
using Shared.DTOs.User;

namespace Shared.Models;

public class RedditPost
{
    public string Title { get; set; }
    public string Body { get; set; }
    public OwnerDto User { get; set; }
    public int Id { get; set; }

    public RedditPost(string Title, string Body, OwnerDto User)
    {
        this.Title = Title;
        this.Body = Body;
        this.User = User;
    }
    
    public override string ToString()
    {
        return $"Title: {Title}, Body: {Body}, User: {User}";
    }
}