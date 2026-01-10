using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Dtos;
using TeamManager.Mappings;

namespace TeamManager.Services;

public class TaskService(AppDbContext context)
{
    public async Task<TaskDto?> GetTaskByIdAsync(int id, CancellationToken ct)
    {
        var task = await context.Tasks.FindAsync([id], ct);
        return task?.ToDto();
    }

    public async Task<List<TaskDto>> GetAllTasksAsync(CancellationToken ct)
    {
        return await context.Tasks
        .AsNoTracking()
        .Select(t => t.ToDto())
        .ToListAsync(ct);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken ct)
    {
        var task = dto.ToEntity();
        context.Tasks.Add(task);
        await context.SaveChangesAsync(ct);
        return task.ToDto();
    }
}