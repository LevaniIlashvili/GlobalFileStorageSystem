using GlobalFileStorageSystem.Domain.Enums;

namespace GlobalFileStorageSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid TenantId { get; set; }

        public string Email { get; set; }
        public UserRole Role { get; set; }
        public Permission Permissions { get; set; }

        public DateTime? LastLoginTimestamp { get; set; }
        public bool MFAEnabled { get; set; }
        
        public string? APIKeyHash { get; set; }
        public TimeSpan SessionTimeout { get; set; }
        public List<string>? IPWhitelist { get; set; }

        public Tenant Tenant { get; set; }
        public List<FileRecord> Files { get; set; }
    }
}
 