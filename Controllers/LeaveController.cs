using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/leave")]
public class LeavesController : ControllerBase
{
    private readonly LeaveService _leaveService;

    public LeavesController(LeaveService leaveService)
    {
        _leaveService = leaveService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaves()
    {
        var leaves = await _leaveService.GetAllLeavesAsync();
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeave(Leave leave)
    {
        var created = await _leaveService.CreateLeaveAsync(leave);
        return Ok(leave);
    }
}