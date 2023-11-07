using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs.RedditPost;
using Shared.Models;

namespace Application.Logic;

public class RedditPostLogic : IRedditPostLogic
{
    private readonly IRedditPostDao redditPostDao;
    private readonly IUserDao userDao;

    public RedditPostLogic(IRedditPostDao redditPostDao, IUserDao userDao)
    {
        this.redditPostDao = redditPostDao;
        this.userDao = userDao;
    }

    public async Task<RedditPost> CreateRedditPostAsync(RedditPostCreationDto dto)
    {
        var user = await userDao.GetByUsername(dto.Owner.Username);
        if (user == null) throw new Exception($"User with id {dto.Owner} was not found.");

        ValidateRedditPost(dto);
        var redditPost = new RedditPost(dto.Title, dto.Body, user);
        var created = await redditPostDao.CreateRedditPostAsync(redditPost);
        return created;
    }

    public async Task<IEnumerable<RedditPost>> GetRedditPostAsync()
    {
        return await redditPostDao.GetRedditPostAsync();
    }

    public async Task UpdateRedditPostAsync(RedditPostUpdateDto redditPostDto)
    {
        var existing = await redditPostDao.GetRedditPostById(redditPostDto.Id);

        if (existing == null) throw new Exception($"RedditPost with ID {redditPostDto.Id} not found!");

        if (string.IsNullOrEmpty(redditPostDto.Body)) throw new Exception("Body cannot be empty.");

        existing.Body = redditPostDto.Body;

        await redditPostDao.UpdateRedditPostAsync(existing);
    }

    public async Task DeleteRedditPost(int id)
    {
        var existing = await redditPostDao.GetRedditPostById(id);
        if (existing == null) throw new Exception($"RedditPost with ID {id} not found!");

        await redditPostDao.DeleteRedditPost(id);
    }

    public async Task<RedditPost> GetRedditPostById(int id)
    {
        return await redditPostDao.GetRedditPostById(id);
    }

    public async Task<ICollection<RedditPost>> GetRedditPostByQuery(string? owner, string? title, string? id)
    {
        return await redditPostDao.GetRedditPostByQueryAsync(owner, title, id);
    }

    private void ValidateRedditPost(RedditPostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    }

    /*private void ValidateRedditPost(RedditPost redditPost)
    {
        if (string.IsNullOrEmpty(redditPost.Title)) throw new Exception("Title cannot be empty.");
    }*/
}