using TeamManager.Data.Enums;

namespace TeamManager.Data.Dtos;

public record UserDto
(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role
);