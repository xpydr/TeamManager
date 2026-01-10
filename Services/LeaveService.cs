using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;

namespace TeamManager.Services;

public class LeaveService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Leave>> GetAllLeavesAsync()
    {
        return await _context.Leaves.ToListAsync();
    }

    public async Task<Leave> CreateLeaveAsync(Leave leave)
    {
        _context.Leaves.Add(leave);
        await _context.SaveChangesAsync();
        return leave;
    }
}
