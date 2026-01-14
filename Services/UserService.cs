using Microsoft.EntityFrameworkCore;
using TeamManager.Data.Dtos;
using TeamManager.Data.Enums;
using TeamManager.Data.Exceptions;
using TeamManager.Data.Mappings;
using TeamManager.Repositories;

namespace TeamManager.Services;

public class UserService(IUserRepository userRepository)
{
    public async Task<UserDto?> GetUserByIdAsync(int id, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(id, ct);
        return user?.ToDto();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken ct = default)
        => (await userRepository.GetAllAsync(ct)).ToDtoList();

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        if (await userRepository.EmailExistsAsync(dto.Email, ct))
            throw new DuplicateEmailException(dto.Email);

        var user = dto.ToEntity();
        await userRepository.AddAsync(user, ct);
        await userRepository.SaveChangesAsync(ct);
        return user.ToDto();
    }

    public async Task<UpdateResult> UpdateUserAsync(int id, UpdateUserDto dto, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(id, ct);

        if (user is null) return UpdateResult.NotFound;
        if (string.IsNullOrWhiteSpace(user.Email)) return UpdateResult.Invalid;

        if (dto.Email is not null) user.Email = dto.Email;
        if (dto.FirstName is not null) user.FirstName = dto.FirstName;
        if (dto.LastName is not null) user.LastName = dto.LastName;
        if (dto.Role.HasValue) user.Role = dto.Role.Value;
        
        userRepository.Update(user);

        try
        {
            await userRepository.SaveChangesAsync(ct);
            return UpdateResult.Success;
        }
        catch (DbUpdateConcurrencyException)
        {
            return UpdateResult.ConcurrencyConflict;
        }
        catch (DbUpdateException)
        {
            return UpdateResult.DatabaseError;
        }
    }

    public async Task<bool> DeleteUserAsync(int id, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(id, ct);
        if (user is null) return false;

        userRepository.Delete(user);        
        await userRepository.SaveChangesAsync(ct);
        return true;
    }
}