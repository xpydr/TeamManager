using TeamManager.Data.Enums;

namespace TeamManager.Data.Dtos;

public record CreateUserDto
(
    string FirstName,
    string LastName,
    string Email,
    UserRole Role
);
