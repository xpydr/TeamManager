using TeamManager.Dtos;

namespace TeamManager.Mappings;

public static class TaskMappings
{
    public static TaskDto ToDto(this Models.Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        return new TaskDto(task.Id, task.Title, task.Description, task.AssignedUserId, task.Status, task.DueDate, task.Priority, task.IsDeleted);
    }

    public static List<TaskDto> ToDtoList(this IEnumerable<Models.Task> tasks)
        => [.. tasks.Select(t => t.ToDto())];

    public static Models.Task ToEntity(this CreateTaskDto dto)
    {
        return new Models.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            AssignedUserId = dto.AssignedUserId,
            Status = dto.Status,
            DueDate = dto.DueDate,
            Priority = dto.Priority
        };
    }
}