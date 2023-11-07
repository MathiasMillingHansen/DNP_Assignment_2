using Shared.DTOs.RedditPost;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IRedditPostDao
{
    Task<RedditPost>CreateRedditPostAsync(RedditPost redditPost);
    Task<IEnumerable<RedditPost>> GetRedditPostAsync();
    Task UpdateRedditPostAsync(RedditPost redditPost);
    Task<RedditPost> GetRedditPostById(int id);
    Task DeleteRedditPost(int id);
    Task<ICollection<RedditPost>> GetRedditPostByQueryAsync(string? owner, string? title, string? id);
}