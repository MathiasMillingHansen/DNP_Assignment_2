using Application.DaoInterfaces;
using Shared.Models;

namespace FileContext.DAOs;

public class RedditPostFileDao : IRedditPostDao
{
    private readonly FileData.FileContext _context;

    public RedditPostFileDao(FileData.FileContext context)
    {
        _context = context;
    }

    public Task<RedditPost> CreateRedditPostAsync(RedditPost redditPost)
    {
        var postId = 1;

        if (_context.Posts != null) postId = _context.Posts.Count + 1;


        redditPost.Id = postId;

        _context.Posts.Add(redditPost);
        _context.SaveChanges();
        _context.UpdateUserPostList(redditPost);

        return Task.FromResult(redditPost);
    }

    /*public Task<RedditPost> CreateRedditPostAsync(RedditPost redditPost)
    {
        context.Posts.Add(redditPost);
        context.SaveChanges();
        return Task.FromResult(redditPost);
    }*/

    public async Task<IEnumerable<RedditPost>> GetRedditPostAsync()
    {
        return await Task.FromResult(_context.Posts);
    }

    public Task UpdateRedditPostAsync(RedditPost redditPostToUpdate)
    {
        var redditPost = _context.Posts.FirstOrDefault(post => post.Id == redditPostToUpdate.Id);
        if (redditPost == null) throw new Exception($"RedditPost with ID {redditPostToUpdate.Id} not found!");
        redditPost.Body = redditPostToUpdate.Body;
        _context.SaveChanges();
        return Task.FromResult(redditPostToUpdate);
    }

    public Task<RedditPost> GetRedditPostById(int id)
    {
        var redditPost = _context.Posts.FirstOrDefault(post => post.Id == id);
        if (redditPost == null) throw new Exception($"RedditPost with ID {id} not found!");
        return Task.FromResult(redditPost);
    }

    public Task DeleteRedditPost(int id)
    {
        var existing = _context.Posts.FirstOrDefault(_redditPosts => _redditPosts.Id == id);
        if (existing == null) throw new Exception($"Post with id {id} does not exist!");

        _context.Posts.Remove(existing);
        _context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task<ICollection<RedditPost>> GetRedditPostByQueryAsync(string? owner, string? title, string? id)
    {
        var redditPostsSortedByQuery = _context.Posts;

        if (!string.IsNullOrEmpty(owner))
            redditPostsSortedByQuery = redditPostsSortedByQuery
                .Where(post => post.User.Username.Contains(owner, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(title))
            redditPostsSortedByQuery = redditPostsSortedByQuery
                .Where(post => post.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(id))
            redditPostsSortedByQuery = redditPostsSortedByQuery
                .Where(post => post.Id.ToString().Contains(id, StringComparison.OrdinalIgnoreCase)).ToList();

        return Task.FromResult(redditPostsSortedByQuery);
    }

    public Task<ICollection<RedditPost>?> GetByUsernameAsync(string username)
    {
        ICollection<RedditPost> postsByUsername = new List<RedditPost>();

        foreach (var _redditPost in _context.Posts)
            if (_redditPost.User.Username.Equals(username))
                postsByUsername.Add(_redditPost);
        return Task.FromResult(postsByUsername);
    }
}