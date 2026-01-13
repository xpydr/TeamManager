using TeamManager.Dtos;
using TeamManager.Mappings;
using TeamManager.Repositories;
using TeamManager.Enums;
using Microsoft.EntityFrameworkCore;

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

    public async Task<UpdateResult> UpdateTaskAsync(int id, UpdateTaskDto dto, CancellationToken ct = default)
    {
        var task = await taskRepository.GetByIdAsync(id, ct);

        if (task is null) return UpdateResult.NotFound;

        if (dto.Title is not null) task.Title = dto.Title;
        if (dto.Description is not null) task.Description = dto.Description;
        if (dto.AssignedUserId.HasValue) task.AssignedUserId = dto.AssignedUserId.Value;
        if (dto.Status.HasValue) task.Status = dto.Status.Value;
        if (dto.DueDate.HasValue) task.DueDate = dto.DueDate.Value;
        if (dto.Priority.HasValue) task.Priority = dto.Priority.Value;
        if (dto.IsDeleted.HasValue) task.IsDeleted = dto.IsDeleted.Value;
        
        taskRepository.Update(task);

        try
        {
            await taskRepository.SaveChangesAsync(ct);
            return UpdateResult.Success;
        }
        catch (DbUpdateConcurrencyException)
        {
            return UpdateResult.ConcurrencyConflict;
        }
        catch (DbUpdateException)
        {
            return UpdateResult.DatabaseError;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id, CancellationToken ct = default)
    {
        var task = await taskRepository.GetByIdAsync(id, ct);
        if (task is null) return false;

        taskRepository.Delete(task);
        await taskRepository.SaveChangesAsync(ct);
        return true;
    }
}