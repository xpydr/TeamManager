using System.ComponentModel.DataAnnotations;
using TeamManager.Data.Interceptors;

namespace TeamManager.Models;

public class User : IAuditableEntity
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = "Employee";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Leave> ?Leaves { get; set; } = [];
    public ICollection<Task> ?Tasks { get; set; } = [];
}
