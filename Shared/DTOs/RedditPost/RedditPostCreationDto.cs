using Shared.DTOs.User;

namespace Shared.DTOs.RedditPost;

public class RedditPostCreationDto
{
    public OwnerDto Owner { get; }
    public string Title { get; }
    public string Body { get; }

    public RedditPostCreationDto(OwnerDto owner, string body, string title)
    {
        Owner = owner;
        Body = body;
        Title = title;
    }
}