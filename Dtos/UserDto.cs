namespace TeamManager.Dtos;

public class UserDto
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string Role { get; init; } = null!;
}