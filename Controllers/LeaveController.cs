using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Dtos;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/leave")]
public class LeavesController(LeaveService leaveService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeaveDto>> GetLeave(int id)
    {
        var leave = await leaveService.GetLeaveByIdAsync(id);
        return leave is null ? NotFound() : Ok(leave);
    }
 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetLeaves()
    {
        var leaves = await leaveService.GetAllLeavesAsync();
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<ActionResult<LeaveDto>> CreateLeave(CreateLeaveDto dto)
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