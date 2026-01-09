namespace TeamManager.Models;

public class Leave
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string Status { get; set; } = "Pending";
}