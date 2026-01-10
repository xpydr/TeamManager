using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Dtos;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/leave")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class LeaveController(LeaveService leaveService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType<LeaveDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveDto>> GetLeave(int id)
    {
        var leave = await leaveService.GetLeaveByIdAsync(id);
        return leave is null ? NotFound() : Ok(leave);
    }

    [ProducesResponseType<IEnumerable<LeaveDto>>(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveDto>>> GetLeaves()
    {
        var leaves = await leaveService.GetAllLeavesAsync();
        return Ok(leaves);
    }

    [HttpPost]
    [ProducesResponseType<LeaveDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LeaveDto>> CreateLeave([FromBody] CreateLeaveDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await leaveService.CreateLeaveAsync(dto);
            return CreatedAtAction(nameof(GetLeave), new { id = created.Id }, created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = ex.Message
            });
        } 
    }
}