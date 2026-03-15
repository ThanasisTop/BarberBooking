using BarberBooking.Core.Entities;
using BarberBooking.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberBooking.Infrastructure.Persistence
{
    public class AppDbContextBase:DbContext
    {
        
        public AppDbContextBase(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public Guid UserId { get; set; }

        //Ignore State property in all entities that implement IEntityState
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(t => typeof(IEntityState).IsAssignableFrom(t.ClrType));

            foreach (var entityType in entityTypes)
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Ignore(nameof(IEntityState.State));
            }

            base.OnModelCreating(modelBuilder);
        }

        // Override SaveChanges to automatically set DateCreated, DateModified, CreatedBy, and ModifiedBy properties
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries<EntityBase>();

            var userId = UserId;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.UtcNow;
                    entry.Entity.DateModified = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.ModifiedBy = userId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DateModified = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = userId;
                }
            }

            return base.SaveChanges();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }

    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
