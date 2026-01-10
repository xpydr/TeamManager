using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;

namespace TeamManager.Services;

public class UserService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
