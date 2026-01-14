using TeamManager.Data.Enums;

namespace TeamManager.Data.Dtos;

public record LeaveDto
(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    LeaveStatus Status
);