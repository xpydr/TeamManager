using Microsoft.EntityFrameworkCore;
using TeamManager.Data;

namespace TeamManager.Services;

public class TaskService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Models.Task>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Models.Task> CreateTaskAsync(Models.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }
}