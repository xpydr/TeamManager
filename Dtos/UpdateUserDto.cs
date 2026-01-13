using System.ComponentModel.DataAnnotations;
using TeamManager.Enums;

namespace TeamManager.Dtos;

public class UpdateUserDto
{
    [EmailAddress]
    [StringLength(256)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    public UserRole? Role { get; set; }
}