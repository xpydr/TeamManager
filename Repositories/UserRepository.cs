using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Data.Models;

using Task = System.Threading.Tasks.Task;

namespace TeamManager.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    void Update(User user);
    void Delete(User user);
    Task<int> SaveChangesAsync(CancellationToken ct);
}

public class UserRepository(AppDbContext context) : IUserRepository
{

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
        => await context.Users.FindAsync([id], ct);

    public async Task<List<User>> GetAllAsync(CancellationToken ct)
        => await context.Users
        .AsNoTracking()
        .ToListAsync(ct);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        => await context.Users.AnyAsync(u => u.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct)
        => await context.Users.AddAsync(user, ct);

    public void Update(User user)
        => context.Users.Update(user);

    public void Delete(User user)
        => context.Users.Remove(user);
        
    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await context.SaveChangesAsync(ct);
}