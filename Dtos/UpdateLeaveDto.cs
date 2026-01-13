using TeamManager.Enums;

namespace TeamManager.Dtos;

public class UpdateLeaveDto
{
    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public LeaveStatus? Status { get; set; }
}