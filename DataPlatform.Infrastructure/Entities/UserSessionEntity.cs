using System.ComponentModel.DataAnnotations;

namespace DataPlatform.Infrastructure.Entities
{
    public class UserSessionEntity : BaseEntity
    {
        [Required]
        public string SessionToken { get; set; } = "";

        public int UserId { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public UserEntity User { get; set; } = null!;
    }
}