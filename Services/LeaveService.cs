using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;

namespace TeamManager.Services;

public class LeaveService
{
    private readonly AppDbContext _context;

    public LeaveService(AppDbContext context)
    {
        _context = context;
    }

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
