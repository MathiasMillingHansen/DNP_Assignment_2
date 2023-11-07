namespace Shared.DTOs.RedditPost;

public class RedditPostUpdateDto
{
    public RedditPostUpdateDto(string body)
    {
        Body = body;
    }

    public int Id { get; set; }
    public string Body { get; set; }
}