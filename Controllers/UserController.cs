using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var created = await _userService.CreateUserAsync(user);
        return Ok(created);
    }
}
