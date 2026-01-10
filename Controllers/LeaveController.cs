using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
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
    public async Task<IActionResult> GetLeaves()
    {
        var leaves = await leaveService.GetAllLeavesAsync();
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeave(Leave leave)
    {
        var created = await leaveService.CreateLeaveAsync(leave);
        return Ok(leave);
    }
}