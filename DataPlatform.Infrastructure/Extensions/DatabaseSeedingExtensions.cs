using DataPlatform.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataPlatform.Infrastructure.Extensions
{
    public static class DatabaseSeedingExtensions
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            
            // Get the specific context type that has migrations (PostgresDbContext or SqlServerDbContext)
            var postgresContext = scope.ServiceProvider.GetService<PostgresDbContext>();
            var sqlServerContext = scope.ServiceProvider.GetService<SqlServerDbContext>();
            
            AppDbContext context = (AppDbContext?)postgresContext ?? (AppDbContext?)sqlServerContext ?? 
                throw new InvalidOperationException("No database context found");
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
            
            try
            {
                // Ensure database is created and migrated
                logger.LogInformation("Ensuring database is created and up to date...");
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migration completed successfully.");
                
                // Now seed the data
                logger.LogInformation("Starting database seeding...");
                var seeder = new DatabaseSeeder(context);
                await seeder.SeedAsync();
                logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during database migration or seeding");
                throw;
            }
        }
    }
}
