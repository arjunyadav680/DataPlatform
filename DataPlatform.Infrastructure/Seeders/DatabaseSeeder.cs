using DataPlatform.Infrastructure.Seeders;

namespace DataPlatform.Infrastructure.Seeders
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            try
            {
                // Ensure database is created
                await _context.Database.EnsureCreatedAsync();

                // Seed languages first (translations depend on them)
                var languageSeeder = new SupportedLanguageSeeder(_context);
                await languageSeeder.SeedAsync();

                // Then seed translations
                var translationSeeder = new TranslationSeeder(_context);
                await translationSeeder.SeedAsync();

                Console.WriteLine("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during database seeding: {ex.Message}");
                throw;
            }
        }
    }
}
