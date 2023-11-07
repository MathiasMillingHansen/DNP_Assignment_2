using Shared.DTOs.RedditPost;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IRedditPostLogic
{
    Task<RedditPost> CreateRedditPostAsync(RedditPostCreationDto dto);
    Task<IEnumerable<RedditPost>> GetRedditPostAsync();
    Task UpdateRedditPostAsync(RedditPostUpdateDto redditPost);
    Task DeleteRedditPost(int id);
    Task<RedditPost> GetRedditPostById(int id);
    Task<ICollection<RedditPost>> GetRedditPostByQuery(string? owner, string? title, string? id);
}