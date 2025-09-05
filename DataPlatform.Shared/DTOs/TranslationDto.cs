namespace DataPlatform.Shared.DTOs
{
    public class TranslationDto
    {
        public long Id { get; set; }
        public string ResourceKey { get; set; } = null!;
        public string Culture { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Context { get; set; }
        public string? Source { get; set; }
        public string? Version { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class CreateTranslationDto
    {
        public string ResourceKey { get; set; } = null!;
        public string Culture { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Context { get; set; }
        public string? Source { get; set; }
        public string? Version { get; set; }
    }

    public class UpdateTranslationDto
    {
        public string Value { get; set; } = null!;
        public string? Context { get; set; }
        public string? Source { get; set; }
        public string? Version { get; set; }
    }

    public class TranslationDictionaryDto
    {
        public string Culture { get; set; } = null!;
        public Dictionary<string, string> Translations { get; set; } = new();
    }
}
