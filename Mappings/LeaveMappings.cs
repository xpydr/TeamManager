using TeamManager.Models;
using TeamManager.Dtos;

namespace TeamManager.Mappings;

public static class LeaveMappings
{
    public static LeaveDto ToDto(this Leave leave)
    {
        ArgumentNullException.ThrowIfNull(leave);

        return new LeaveDto(leave.Id, leave.UserId, leave.StartDate, leave.EndDate, leave.Status);
    }

    public static List<LeaveDto> ToDtoList(this IEnumerable<Leave> leaves)
        => [.. leaves.Select(l => l.ToDto())];

    public static Leave ToEntity(this CreateLeaveDto dto)
    {
        return new Leave
        {
            UserId = dto.UserId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status
        };
    }
}