namespace TeamManager.Dtos;

public record TaskDto
(
    int Id,
    string Title,
    string Description,
    int AssignedUserId,
    Enums.TaskStatus Status
);