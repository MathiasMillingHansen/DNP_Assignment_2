using Shared.DTOs.User;

namespace Shared.DTOs.RedditPost;

public class RedditPostDto
{
    public RedditPostDto(string Title, string Body, OwnerDto User, int id)
    {
        this.Title = Title;
        this.Body = Body;
        this.User = User;
        Id = id;
    }

    public string Title { get; set; }
    public string Body { get; set; }
    public OwnerDto User { get; set; }
    public int Id { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}, Body: {Body}, User: {User}";
    }
}