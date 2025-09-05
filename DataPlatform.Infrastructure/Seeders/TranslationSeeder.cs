using System.Text.Json;
using DataPlatform.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataPlatform.Infrastructure.Seeders
{
    public class TranslationSeeder
    {
        private readonly AppDbContext _context;
        private readonly string _seedDataPath;

        public TranslationSeeder(AppDbContext context)
        {
            _context = context;
            _seedDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "translations");
        }

        public async Task SeedAsync()
        {
            // Get enabled languages from database
            var enabledLanguages = await _context.Languages
                .Where(l => l.Enabled)
                .Select(l => l.Culture)
                .ToListAsync();

            foreach (var culture in enabledLanguages)
            {
                var filePath = Path.Combine(_seedDataPath, $"{culture}.json");
                
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Translation file not found: {filePath}");
                    continue;
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                if (translations == null) continue;

                foreach (var kvp in translations)
                {
                    var resourceKey = kvp.Key;
                    var value = kvp.Value;

                    var existing = await _context.Translations
                        .FirstOrDefaultAsync(t => t.ResourceKey == resourceKey && t.Culture == culture);

                    if (existing == null)
                    {
                        // Create new translation
                        var newTranslation = new TranslationEntity
                        {
                            ResourceKey = resourceKey,
                            Culture = culture,
                            Value = value,
                            Source = "seed"
                        };

                        _context.Translations.Add(newTranslation);
                    }
                    else
                    {
                        // Update existing translation if values have changed and source allows it
                        var forceFromSeed = Environment.GetEnvironmentVariable("FORCE_FROM_SEED")?.ToLower() == "true";
                        
                        if (existing.Source != "admin" || forceFromSeed)
                        {
                            if (existing.Value != value)
                            {
                                existing.Value = value;
                                existing.Source = "seed";
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
