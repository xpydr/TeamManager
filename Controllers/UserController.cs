using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamManager.Data.Dtos;
using TeamManager.Data.Enums;
using TeamManager.Data.Exceptions;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/users")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser(int id, CancellationToken ct = default)
    {
        var user = await userService.GetUserByIdAsync(id, ct);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<UserDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken ct = default)
    {
        var users = await userService.GetAllUsersAsync(ct);
        return Ok(users);
    }

    [HttpPost]
    [ProducesResponseType<UserDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await userService.CreateUserAsync(dto, ct);
            return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = ex.Message
            });
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(new ProblemDetails 
            {
                Title = "Email already exists",
                Detail = ex.Message 
            });
        }
    }

    [HttpPut("{id}")]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto, CancellationToken ct = default)
    {
        var result = await userService.UpdateUserAsync(id, dto, ct);

        return result switch
        {
            UpdateResult.Success => NoContent(),
            UpdateResult.NotFound => NotFound(),
            UpdateResult.Invalid => BadRequest(),
            UpdateResult.ConcurrencyConflict => Conflict(),
            UpdateResult.DatabaseError => UnprocessableEntity(),
            _ => StatusCode(500)
        };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await userService.DeleteUserAsync(id);
        return success ? NoContent() : NotFound();
    }
}