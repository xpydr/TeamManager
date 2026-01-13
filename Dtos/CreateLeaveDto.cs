using System.ComponentModel.DataAnnotations;
using TeamManager.Enums;

namespace TeamManager.Dtos;

public record CreateLeaveDto
(
    [Required]
    [Range(0, int.MaxValue)]
    int UserId,

    [Required]
    DateOnly StartDate,

    [Required]
    DateOnly EndDate
);