using TeamManager.Dtos;
using TeamManager.Mappings;
using TeamManager.Repositories;

namespace TeamManager.Services;

public class TaskService(ITaskRepository taskRepository)
{
    public async Task<TaskDto?> GetTaskByIdAsync(int id, CancellationToken ct = default)
    {
        var task = await taskRepository.GetByIdAsync(id, ct);
        return task?.ToDto();
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(CancellationToken ct = default)
        => (await taskRepository.GetAllAsync(ct)).ToDtoList();

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken ct = default)
    {
        var task = dto.ToEntity();
        await taskRepository.AddAsync(task, ct);
        await taskRepository.SaveChangesAsync(ct);
        return task.ToDto();
    }

    public async Task<bool> DeleteTaskAsync(int id, CancellationToken ct = default)
    {
        var task = await taskRepository.GetByIdAsync(id, ct);
        if (task is null) return false;

        await taskRepository.DeleteAsync(task, ct);
        await taskRepository.SaveChangesAsync(ct);
        return true;
    }
}