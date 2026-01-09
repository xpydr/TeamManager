using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamManager.Models;

namespace TeamManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Leave> Leaves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.Property(e => e.Status)
                  .HasConversion<string>();

            entity.ToTable(tb => tb.HasCheckConstraint(
                "CK_Leave_Status_Valid",
                "[Status] IN ('Pending', 'Approved', 'Denied')"));
        });
    }
}
