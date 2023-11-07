using System.Security.Claims;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.RedditPost;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RedditPostController : ControllerBase
{
    private readonly IRedditPostLogic _redditPostLogic;

    public RedditPostController(IRedditPostLogic redditPostLogic)
    {
        _redditPostLogic = redditPostLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<RedditPost>> CreateAsync(RedditPostCreationDto dto)
    {
        try
        {
            RedditPost redditPost = await _redditPostLogic.CreateRedditPostAsync(dto);
            return Created($"/posts/{redditPost.Id}", redditPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /*[HttpGet]
    public async Task<ActionResult<IEnumerable<RedditPost>>> GetAllAsync()
    {
        try
        {
            IEnumerable<RedditPost> redditPosts = await _redditPostLogic.GetRedditPostAsync();
            return Ok(redditPosts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }*/

    [HttpGet]
    public async Task<ActionResult<ICollection<RedditPost>>> GetFromQuery(string? owner, string? title, string? id)
    {
        try
        {
            ICollection<RedditPost> redditPosts = await _redditPostLogic.GetRedditPostByQuery(owner,title,id);
            return Ok(redditPosts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchAsync(RedditPostUpdateDto dto)
    {
        try
        {
            await _redditPostLogic.UpdateRedditPostAsync(dto);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<RedditPost>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            Claim? claims = User.Claims.FirstOrDefault();
            Console.WriteLine(claims?.Value);
            RedditPost redditPost = await _redditPostLogic.GetRedditPostById(id);
            return Ok(redditPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _redditPostLogic.DeleteRedditPost(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}