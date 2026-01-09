using Microsoft.EntityFrameworkCore;
using TeamManager.Models;

namespace TeamManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Leave> Leaves { get; set; }
}
