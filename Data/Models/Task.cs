using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamManager.Data.Interceptors;

namespace TeamManager.Data.Models;

public class Task : IAuditableEntity
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [StringLength(4000)]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(AssignedUser))]
    public int AssignedUserId { get; set; }
    
    public User? AssignedUser { get; set; }

    [Required]
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

    public DateTime? DueDate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int Priority { get; set; } = 0;

    public bool IsDeleted { get; set; } = false;
}