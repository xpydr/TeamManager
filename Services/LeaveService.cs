using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;
using TeamManager.Dtos;
using TeamManager.Mappings;

namespace TeamManager.Services;

public class LeaveService(AppDbContext context)
{
 
    public async Task<List<LeaveDto>> GetAllLeavesAsync()
    {
        return await context.Leaves
        .AsNoTracking()
        .Select(l => l.ToDto())
        .ToListAsync();
    }

    public async Task<LeaveDto> CreateLeaveAsync(CreateLeaveDto dto)
    {
        var leave = dto.ToEntity();
        context.Leaves.Add(leave);
        await context.SaveChangesAsync();
        return leave.ToDto();
    }
}
