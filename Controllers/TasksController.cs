using Microsoft.AspNetCore.Mvc;
using TeamManager.Services;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Models.Task task)
    {
        var created = await _taskService.CreateTaskAsync(task);
        return Ok(created);
    }
}
