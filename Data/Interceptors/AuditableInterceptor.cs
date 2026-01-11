using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TeamManager.Data.Interceptors
{
    /// <summary>
    /// Automatically maintains audit timestamps (CreatedAt / UpdatedAt) for entities 
    /// implementing <see cref="IAuditableEntity"/>.
    /// </summary>
    public sealed class AuditableInterceptor : SaveChangesInterceptor
    {
        /// <summary>
        /// Intercepts before saving changes asynchronously.
        /// </summary>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken ct = default)
        {
            if (eventData.Context is not { } context)
                return base.SavingChangesAsync(eventData, result, ct);

            var auditableEntries = context.ChangeTracker
                .Entries<IAuditableEntity>()
                .Where(e => e.State is EntityState.Added or EntityState.Modified);

            if (!auditableEntries.Any())
                return base.SavingChangesAsync(eventData, result, ct);

            var now = DateTime.UtcNow;

            foreach (var entry in auditableEntries)
            {
                var entity = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }
                else // EntityState.Modified
                {
                    entity.UpdatedAt = now;
                }

                // Prevent CreatedAt from being overwritten
                entry.Property(e => e.CreatedAt).IsModified = false;
            }

            return base.SavingChangesAsync(eventData, result, ct);
        }
    }

    /// <summary>
    /// Marker interface for entities that should have automatic audit timestamps
    /// </summary>
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}