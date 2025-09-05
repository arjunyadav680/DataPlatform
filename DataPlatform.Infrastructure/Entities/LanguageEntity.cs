using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataPlatform.Infrastructure.Entities
{
    public class LanguageEntity : BaseEntity
    {
        // Culture code, e.g. "en", "en-US", "hi-IN"
        public string Culture { get; set; } = null!;
        // Friendly name in English: "English", "Hindi"
        public string DisplayName { get; set; } = null!;
        // Native name: "English", "हिन्दी"
        public string? NativeName { get; set; }
        // "ltr" or "rtl"
        public string Direction { get; set; } = "ltr";
        // Whether this language is active/visible in UI
        public bool Enabled { get; set; } = true;
        // UI ordering weight (lower = earlier)
        public int Priority { get; set; } = 100;
        // Optional notes/source
        public string? Source { get; set; }
    }
}
