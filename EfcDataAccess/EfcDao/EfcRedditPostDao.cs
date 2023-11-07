using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess.EfcDao;

public class EfcRedditPostDao : IRedditPostDao
{
    
    private readonly EfcRedditContext _context;
    
    public EfcRedditPostDao(EfcRedditContext context)
    {
        _context = context;
    }
    
    public async Task<RedditPost> CreateRedditPostAsync(RedditPost redditPost)
    {
        EntityEntry<RedditPost> newRedditPost = await _context.RedditPosts.AddAsync(redditPost);
        await _context.SaveChangesAsync();
        return newRedditPost.Entity;
    }

    public async Task<IEnumerable<RedditPost>> GetRedditPostAsync()
    {
        ICollection<RedditPost> redditPostsToReturn = await _context.RedditPosts.ToListAsync();
        return redditPostsToReturn;
    }

    public async Task UpdateRedditPostAsync(RedditPost redditPost)
    {
        _context.RedditPosts.Update(redditPost);
        await _context.SaveChangesAsync();
    }

    public async Task<RedditPost> GetRedditPostById(int id)
    {
        RedditPost? existing = await _context.RedditPosts.FirstOrDefaultAsync(u =>
            u.Id.Equals(id)
        );
        return existing!;
    }

    public async Task DeleteRedditPost(int id)
    {
        RedditPost? postToDelete = await GetRedditPostById(id);
        _context.RedditPosts.Remove(postToDelete!);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<RedditPost>> GetRedditPostByQueryAsync(string? owner, string? title, string? id)
    {
        ICollection<RedditPost> posts;
        if (owner != null)
        {
            posts = await _context.RedditPosts.Where(p => p.User.Username.ToLower().Equals(owner.ToLower())).ToListAsync();
        }
        if (title != null)
        {
            posts = await _context.RedditPosts.Where(p => p.Title.ToLower().Equals(title.ToLower())).ToListAsync();
        }
        if (id != null)
        {
            posts = await _context.RedditPosts.Where(p => p.Id.Equals(id)).ToListAsync();
        }
        else
        {
            posts = await _context.RedditPosts.ToListAsync();
        }

        return posts;
    }
}