namespace TeamManager.Dtos;

public class CreateUserDto
{
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}