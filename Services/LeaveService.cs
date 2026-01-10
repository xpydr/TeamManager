using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;
using TeamManager.Dtos;
using TeamManager.Mappings;

namespace TeamManager.Services;

public class LeaveService(AppDbContext context)
{
    public async Task<LeaveDto?> GetLeaveByIdAsync(int id, CancellationToken ct)
    {
        var leave = await context.Leaves.FindAsync([id], ct);
        return leave?.ToDto();
    }

    public async Task<List<LeaveDto>> GetAllLeavesAsync(CancellationToken ct)
    {
        return await context.Leaves
        .AsNoTracking()
        .Select(l => l.ToDto())
        .ToListAsync(ct);
    }

    public async Task<LeaveDto> CreateLeaveAsync(CreateLeaveDto dto, CancellationToken ct)
    {
        var leave = dto.ToEntity();
        context.Leaves.Add(leave);
        await context.SaveChangesAsync(ct);
        return leave.ToDto();
    }
}