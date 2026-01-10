using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Mappings;
using TeamManager.Dtos;

namespace TeamManager.Services;

public class UserService(AppDbContext context)
{
    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        return user?.ToDto();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        return await context.Users
        .AsNoTracking()
        .Select(u => u.ToDto())
        .ToListAsync();
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        var user = dto.ToEntity();
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user.ToDto();
    }
}