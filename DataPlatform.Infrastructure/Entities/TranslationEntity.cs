using System.ComponentModel.DataAnnotations;

namespace DataPlatform.Infrastructure.Entities
{
    public class TranslationEntity : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string ResourceKey { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Culture { get; set; } = null!;

        [Required]
        public string Value { get; set; } = null!;

        [MaxLength(100)]
        public string? Context { get; set; }

        [MaxLength(50)]
        public string? Source { get; set; }

        [MaxLength(20)]
        public string? Version { get; set; }
    }
}
