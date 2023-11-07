namespace Shared.DTOs.RedditPost;

public class RedditPostUpdateDto
{
    public int Id { get; set; }
    public string Body { get; set; }

    public RedditPostUpdateDto(string body)
    {
        Body = body;
    }
}