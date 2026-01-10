using TeamManager.Enums;

namespace TeamManager.Dtos;

public record LeaveDto
(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    LeaveStatus Status
);