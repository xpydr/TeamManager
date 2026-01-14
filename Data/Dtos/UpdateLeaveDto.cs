using TeamManager.Data.Enums;

namespace TeamManager.Data.Dtos;

public class UpdateLeaveDto
{
    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public LeaveStatus? Status { get; set; }
}