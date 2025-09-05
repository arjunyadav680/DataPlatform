using System.Text.Json;
using DataPlatform.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataPlatform.Infrastructure.Seeders
{
    public class SupportedLanguageSeeder
    {
        private readonly AppDbContext _context;
        private readonly string _seedDataPath;

        public SupportedLanguageSeeder(AppDbContext context)
        {
            _context = context;
            _seedDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData");
        }

        public async Task SeedAsync()
        {
            var filePath = Path.Combine(_seedDataPath, "supported-languages.json");
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not found: {filePath}");
                return;
            }

            var jsonContent = await File.ReadAllTextAsync(filePath);
            var languages = JsonSerializer.Deserialize<SupportedLanguageDto[]>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (languages == null) return;

            foreach (var languageDto in languages)
            {
                var existing = await _context.Languages
                    .FirstOrDefaultAsync(l => l.Culture == languageDto.Culture);

                if (existing == null)
                {
                    // Create new language
                    var newLanguage = new LanguageEntity
                    {
                        Culture = languageDto.Culture,
                        DisplayName = languageDto.DisplayName,
                        NativeName = languageDto.NativeName,
                        Direction = languageDto.Direction,
                        Enabled = languageDto.Enabled,
                        Priority = languageDto.Priority,
                        Source = languageDto.Source
                    };

                    _context.Languages.Add(newLanguage);
                }
                else
                {
                    // Update existing language if values have changed and source allows it
                    var forceFromSeed = Environment.GetEnvironmentVariable("FORCE_FROM_SEED")?.ToLower() == "true";
                    
                    if (existing.Source != "admin" || forceFromSeed)
                    {
                        existing.DisplayName = languageDto.DisplayName;
                        existing.NativeName = languageDto.NativeName;
                        existing.Direction = languageDto.Direction;
                        existing.Enabled = languageDto.Enabled;
                        existing.Priority = languageDto.Priority;
                        existing.Source = languageDto.Source;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private class SupportedLanguageDto
        {
            public string Culture { get; set; } = null!;
            public string DisplayName { get; set; } = null!;
            public string? NativeName { get; set; }
            public string Direction { get; set; } = "ltr";
            public bool Enabled { get; set; } = true;
            public int Priority { get; set; } = 100;
            public string? Source { get; set; }
        }
    }
}
