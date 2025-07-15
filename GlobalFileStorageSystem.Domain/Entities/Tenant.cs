using GlobalFileStorageSystem.Domain.Enums;

namespace GlobalFileStorageSystem.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string OrganizationName { get; set; }
        public string SubdomainPrefix { get; set; }
        public TenantStatus TenantStatus { get; set; }
        public BillingPlan BillingPlan { get; set; }
        public long StorageQuota { get; set; } // in bytes
        public long BandwithQuota { get; set; } // in bytes
        public int APIRateLimit { get; set; } // requests per minute
        public string DataResidencyRegion { get; set; }
        public ComplianceRequirement ComplianceRequirements { get; set; }
        public EncriptionRequirement EncryptionRequirements { get; set; }

        public List<User> Users { get; set; }
        public List<FileRecord> Files { get; set; }
        public List<UsageRecord> Usages { get; set; }
    }
}
