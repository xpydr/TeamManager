using System.ComponentModel.DataAnnotations;
using TeamManager.Data.Interceptors;
using TeamManager.Enums;

namespace TeamManager.Models;

public class Leave : IValidatableObject, IAuditableEntity
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public User User { get; set; } = null!;

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
        {
            yield return new ValidationResult(
                "The start date must be on or before the end date.",
                [nameof(StartDate), nameof(EndDate)]);
        }
    }
}