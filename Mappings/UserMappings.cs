using TeamManager.Models;
using TeamManager.Dtos;

namespace TeamManager.Mappings;

public static class UserMappings
{
    public static UserDto ToDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDto(user.Id, user.Email, user.Role);
    }

    public static List<UserDto> ToDtoList(this IEnumerable<User> users)
        => [.. users.Select(u => u.ToDto())];

    public static User ToEntity(this CreateUserDto dto)
    {
        return new User
        {
            Email = dto.Email,
            Role = dto.Role
        };
    }
}