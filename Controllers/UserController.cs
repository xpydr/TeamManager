using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
using TeamManager.Services;
using TeamManager.Mappings;
using TeamManager.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AppDbContext _context;

    public UserController(UserService userService, AppDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return Ok(user.ToDto());
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
