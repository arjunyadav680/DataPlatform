namespace DataPlatform.Shared.DTOs
{
    public class LanguageDto
    {
        public long Id { get; set; }
        public string Culture { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? NativeName { get; set; }
        public string Direction { get; set; } = "ltr";
        public bool Enabled { get; set; } = true;
        public int Priority { get; set; } = 100;
        public string? Source { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class CreateLanguageDto
    {
        public string Culture { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? NativeName { get; set; }
        public string Direction { get; set; } = "ltr";
        public bool Enabled { get; set; } = true;
        public int Priority { get; set; } = 100;
        public string? Source { get; set; }
    }

    public class UpdateLanguageDto
    {
        public string DisplayName { get; set; } = null!;
        public string? NativeName { get; set; }
        public string Direction { get; set; } = "ltr";
        public bool Enabled { get; set; } = true;
        public int Priority { get; set; } = 100;
        public string? Source { get; set; }
    }
}
