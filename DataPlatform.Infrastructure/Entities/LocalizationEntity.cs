using System.ComponentModel.DataAnnotations;

namespace DataPlatform.Infrastructure.Entities
{
    public class LocalizationEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Key { get; set; } = "";

        [Required]
        [MaxLength(10)]
        public string Language { get; set; } = "";

        [Required]
        public string Value { get; set; } = "";
    }
}