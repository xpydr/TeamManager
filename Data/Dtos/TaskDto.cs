namespace TeamManager.Data.Dtos;

public record TaskDto
(
    int Id,
    string Title,
    string? Description,
    int AssignedUserId,
    Enums.TaskStatus Status,
    DateTime? DueDate,
    int Priority,
    bool IsDeleted
);