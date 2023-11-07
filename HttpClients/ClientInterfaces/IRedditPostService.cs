using Shared.DTOs.RedditPost;
using Shared.Models;

namespace Clients.ClientInterfaces;

public interface IRedditPostService
{
    Task CreatePostAsync(RedditPostCreationDto postToCreate);
    Task <ICollection<RedditPost>> GetPostsAsync(
        string? owner,
        string? title,
        string? id
        );
    
    Task<RedditPostDto> GetPostByIdAsync(int id);
    Task UpdateAsync(RedditPostUpdateDto dto);
    Task DeletePostAsync(int id);
}