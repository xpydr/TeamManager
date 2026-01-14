using Microsoft.EntityFrameworkCore;
using TeamManager.Data;

namespace TeamManager.Repositories;

public interface ITaskRepository
{
    Task<Data.Models.Task?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Data.Models.Task>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Data.Models.Task task, CancellationToken ct);
    void Update(Data.Models.Task task);
    Task<int> SaveChangesAsync(CancellationToken ct);
    void Delete(Data.Models.Task task);
}

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<Data.Models.Task?> GetByIdAsync(int id, CancellationToken ct)
        => await context.Tasks.FindAsync([id], ct);
    
    public async Task<List<Data.Models.Task>> GetAllAsync(CancellationToken ct)
        => await context.Tasks
        .AsNoTracking()
        .ToListAsync(ct);

    public async Task AddAsync(Data.Models.Task task, CancellationToken ct)
        => await context.Tasks.AddAsync(task, ct);
    
    public void Update(Data.Models.Task task)
        => context.Tasks.Update(task);

    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await context.SaveChangesAsync(ct);

    public void Delete(Data.Models.Task task)
        => context.Tasks.Remove(task);
}