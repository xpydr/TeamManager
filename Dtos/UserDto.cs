namespace TeamManager.Dtos;

public record UserDto
(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
);