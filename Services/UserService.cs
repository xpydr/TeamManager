using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Mappings;
using TeamManager.Dtos;
using TeamManager.Exceptions;

namespace TeamManager.Services;

public class UserService(AppDbContext context)
{
    public async Task<UserDto?> GetUserByIdAsync(int id, CancellationToken ct)
    {
        var user = await context.Users.FindAsync([id], ct);
        return user?.ToDto();
    }

    public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken ct)
    {
        return await context.Users
        .AsNoTracking()
        .Select(u => u.ToDto())
        .ToListAsync(ct);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto, CancellationToken ct)
    {
        var existing = await context.Users
            .AnyAsync(u => u.Email == dto.Email, ct);

        if (existing)
        {
            throw new DuplicateEmailException(dto.Email);
        }

        var user = dto.ToEntity();
        context.Users.Add(user);
        await context.SaveChangesAsync(ct);
        return user.ToDto();
    }
}