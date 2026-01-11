using Microsoft.EntityFrameworkCore;
using TeamManager.Data;

namespace TeamManager.Repositories;

public interface ITaskRepository
{
    Task<Models.Task?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Models.Task>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Models.Task task, CancellationToken ct);
    Task<int> SaveChangesAsync(CancellationToken ct);
}

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<Models.Task?> GetByIdAsync(int id, CancellationToken ct)
        => await context.Tasks.FindAsync([id], ct);
    
    public async Task<List<Models.Task>> GetAllAsync(CancellationToken ct)
        => await context.Tasks
        .AsNoTracking()
        .ToListAsync(ct);

    public async Task AddAsync(Models.Task task, CancellationToken ct)
        => await context.Tasks.AddAsync(task, ct);

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => context.SaveChangesAsync(ct);
}