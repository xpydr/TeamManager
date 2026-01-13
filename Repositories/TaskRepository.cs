using Microsoft.EntityFrameworkCore;
using TeamManager.Data;

namespace TeamManager.Repositories;

public interface ITaskRepository
{
    Task<Models.Task?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Models.Task>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Models.Task task, CancellationToken ct);
    void Update(Models.Task task);
    Task<int> SaveChangesAsync(CancellationToken ct);
    void Delete(Models.Task task);
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
    
    public void Update(Models.Task task)
        => context.Tasks.Update(task);

    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await context.SaveChangesAsync(ct);

    public void Delete(Models.Task task)
        => context.Tasks.Remove(task);
}