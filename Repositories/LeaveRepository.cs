using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;

using Task = System.Threading.Tasks.Task;

namespace TeamManager.Repositories;

public interface ILeaveRepository
{
    Task<Leave?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Leave>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Leave leave, CancellationToken ct);
    Task<int> SaveChangesAsync(CancellationToken ct);
    void Delete(Leave leave);
}

public class LeaveRepository(AppDbContext context) : ILeaveRepository
{
    public async Task<Leave?> GetByIdAsync(int id, CancellationToken ct)
        => await context.Leaves.FindAsync([id], ct);
    
    public async Task<List<Leave>> GetAllAsync(CancellationToken ct)
        => await context.Leaves
        .AsNoTracking()
        .ToListAsync(ct);

    public async Task AddAsync(Leave leave, CancellationToken ct)
        => await context.Leaves.AddAsync(leave, ct);
    
    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await context.SaveChangesAsync(ct);

    public void Delete(Leave leave)
        => context.Leaves.Remove(leave);
}