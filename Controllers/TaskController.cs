using Microsoft.AspNetCore.Mvc;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController(TaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Models.Task task)
    {
        var created = await taskService.CreateTaskAsync(task);
        return Ok(created);
    }
}
