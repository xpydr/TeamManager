using TeamManager.Data.Dtos;

namespace TeamManager.Data.Mappings;

public static class TaskMappings
{
    public static TaskDto ToDto(this Data.Models.Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        return new TaskDto(task.Id, task.Title, task.Description, task.AssignedUserId, task.Status, task.DueDate, task.Priority, task.IsDeleted);
    }

    public static List<TaskDto> ToDtoList(this IEnumerable<Data.Models.Task> tasks)
        => [.. tasks.Select(t => t.ToDto())];

    public static Data.Models.Task ToEntity(this CreateTaskDto dto)
    {
        return new Data.Models.Task
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