using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamManager.Models;


public class Leave : IValidatableObject
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Status { get; set; } = "Pending";

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