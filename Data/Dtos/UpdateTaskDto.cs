using System.ComponentModel.DataAnnotations;

namespace TeamManager.Data.Dtos;

public class UpdateTaskDto
{
    [StringLength(200, MinimumLength = 2)]
    public string? Title { get; set; }

    [StringLength(4000)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int? AssignedUserId { get; set; }

    public Enums.TaskStatus? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public int? Priority { get; set; }

    public bool? IsDeleted { get; set; }
}