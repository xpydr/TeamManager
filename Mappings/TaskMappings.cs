using TeamManager.Dtos;

namespace TeamManager.Mappings;

public static class TaskMappings
{
    public static TaskDto ToDto(this Models.Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        return new TaskDto(task.Id, task.Title, task.Description, task.AssignedUserId, task.Status);
    }

    public static Models.Task ToEntity(this CreateTaskDto dto)
    {
        return new Models.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            AssignedUserId = dto.AssignedUserId,
            Status = dto.Status
        };
    }
}