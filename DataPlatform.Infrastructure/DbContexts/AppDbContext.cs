using DataPlatform.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataPlatform.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TranslationEntity> Translations { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "system"; // You can get this from current user context
                        entry.Entity.UpdatedAt = null;
                        entry.Entity.UpdatedBy = null;
                        break;
                    
                    case EntityState.Modified:
                        // Prevent modification of audit creation fields
                        entry.Property(e => e.CreatedAt).IsModified = false;
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "system"; // You can get this from current user context
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Translation unique constraint
            modelBuilder.Entity<TranslationEntity>()
                .HasIndex(t => new { t.ResourceKey, t.Culture })
                .IsUnique();

            // Language unique constraint
            modelBuilder.Entity<LanguageEntity>()
                .HasIndex(l => l.Culture)
                .IsUnique();

        }
           
    }

    public class SqlServerDbContext : AppDbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options) { }
    }

    public class PostgresDbContext : AppDbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options) { }
    }

}
