using Microsoft.AspNetCore.Mvc;
using TeamManager.Services;
using TeamManager.Dtos;
using System.ComponentModel.DataAnnotations;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/tasks")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class TaskController(TaskService taskService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTask(int id, CancellationToken ct = default)
    {
        var result = await taskService.GetTaskByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<TaskDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(CancellationToken ct = default)
    {
        var tasks = await taskService.GetAllTasksAsync(ct);
        return Ok(tasks);
    }

    [HttpPost]
    [ProducesResponseType<TaskDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto dto, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await taskService.CreateTaskAsync(dto, ct);
            return CreatedAtAction(nameof(GetTask), new { id = created.Id }, created);
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var success = await taskService.DeleteTaskAsync(id);
        return success ? NoContent() : NotFound();
    }
}
