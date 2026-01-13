using TeamManager.Enums;

namespace TeamManager.Dtos;

public record CreateUserDto
(
    string FirstName,
    string LastName,
    string Email,
    UserRole Role
);
