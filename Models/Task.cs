namespace TeamManager.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AssignedUserId { get; set; }
    public User AssignedUser { get; set; } = null!;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;
}