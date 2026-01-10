using System.ComponentModel.DataAnnotations;
using TeamManager.Enums;

namespace TeamManager.Models;

public class Leave : IValidatableObject
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

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