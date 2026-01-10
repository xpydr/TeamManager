using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
using TeamManager.Services;
using TeamManager.Dtos;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var result = await userService.GetUserByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var created = await userService.CreateUserAsync(user);
        return Ok(created);
    }
}