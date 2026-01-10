namespace TeamManager.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AssignedUserId { get; set; }
    public string Status { get; set; } = "Pending";
}