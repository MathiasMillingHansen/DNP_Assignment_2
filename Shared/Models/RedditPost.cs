using System.ComponentModel.DataAnnotations;
using Shared.DTOs;
using Shared.DTOs.User;

namespace Shared.Models;

public class RedditPost
{
    public string Title { get; private set; }
    public string Body { get; set; }
    
    public OwnerDto User { get; private set; }
    public int Id { get; set; }

    public RedditPost(string Title, string Body, OwnerDto User)
    {
        this.Title = Title;
        this.Body = Body;
        this.User = User;
    }
    
    public RedditPost()
    {
        
    }
    
    public override string ToString()
    {
        return $"Title: {Title}, Body: {Body}, User: {User}";
    }
}