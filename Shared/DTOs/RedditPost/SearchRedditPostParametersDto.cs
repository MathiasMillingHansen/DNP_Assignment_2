namespace Shared.DTOs.RedditPost;

public class SearchRedditPostParametersDto
{
    public string? Username { get;}
    public int? RedditPostId { get;}
    public string? TitleContains { get;}

    public SearchRedditPostParametersDto(string? username, int? redditPostId, string? titleContains)
    {
        Username = username;
        RedditPostId = redditPostId;
        TitleContains = titleContains;
    }
}