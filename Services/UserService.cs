using TeamManager.Mappings;
using TeamManager.Dtos;
using TeamManager.Exceptions;
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

    public async Task<bool> DeleteUserAsync(int id, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(id, ct);
        if (user is null) return false;

        await userRepository.DeleteAsync(user, ct);        
        await userRepository.SaveChangesAsync(ct);
        return true;
    }
}