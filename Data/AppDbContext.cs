using Microsoft.EntityFrameworkCore;
using TeamManager.Data.Models;

namespace TeamManager.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Data.Models.Task> Tasks { get; set; }
    public DbSet<Leave> Leaves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Role).HasConversion<string>();

            entity.ToTable(tb => tb.HasCheckConstraint(
                "CK_User_Role_Valid",
                "[Role] IN ('Employee', 'Admin')"));
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.Property(e => e.Status).HasConversion<string>();

            entity.ToTable(tb => tb.HasCheckConstraint(
                "CK_Leave_Status_Valid",
                "[Status] IN ('Pending', 'Approved', 'Denied')"));
        });

        modelBuilder.Entity<Data.Models.Task>(entity =>
        {
            entity.Property(e => e.Status).HasConversion<string>();

            entity.ToTable(tb => tb.HasCheckConstraint(
                "CK_Leave_Status_Valid",
                "[Status] IN ('Pending', 'InProgress', 'Completed')"));
        });
    }
}
