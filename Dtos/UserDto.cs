using TeamManager.Enums;

namespace TeamManager.Dtos;

public record UserDto
(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role
);