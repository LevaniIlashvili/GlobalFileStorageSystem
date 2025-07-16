using GlobalFileStorageSystem.Domain.Enums;

namespace GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant
{
    public class TenantViewmodel
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
    }
}
