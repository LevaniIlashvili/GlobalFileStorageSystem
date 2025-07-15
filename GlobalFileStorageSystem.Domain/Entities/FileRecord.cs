using GlobalFileStorageSystem.Domain.Enums;

namespace GlobalFileStorageSystem.Domain.Entities
{
    public class FileRecord : BaseEntity
    {
        public Guid TenantId { get; set; }

        public string FileName { get; set; }
        public long FileSize { get; set; } // in bytes
        public string ContentType { get; set; }
        public string StoragePath  { get; set; }

        public string MD5Hash { get; set; }
        public string SHA256Hash { get; set; }
        public string? EncryptionKeyId { get; set; }

        public DateTime UploadTimestamp { get; set; }
        public DateTime? LastAccessedTimestamp { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public int VersionNumber { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public List<string> Tags { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public Guid UploadedBy { get; set; }
    
        public bool IsRestricted => AccessLevel == AccessLevel.Restricted;

        public User User { get; set; }
        public Tenant Tenant { get; set; }
    }
}
