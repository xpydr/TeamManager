using TeamManager.Enums;

namespace TeamManager.Dtos;

public record CreateLeaveDto
(
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    LeaveStatus Status
);