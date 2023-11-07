using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.DTOs.User;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userlogic)
    {
        _userLogic = userlogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync([FromBody] UserCreationDto dto)
    {
        try
        {
            var user = await _userLogic.CreateUserAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<GetUserDto>>> GetAllAsync()
    {
        try
        {
            var users = await _userLogic.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<GetUserWithPasswordDto>> GetByUsernameAsync(string username)
    {
        try
        {
            var user = await _userLogic.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}