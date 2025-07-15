namespace GlobalFileStorageSystem.Domain.Entities
{
    public class UsageRecord : BaseEntity
    {
        public Guid TenantId { get; set; }

        public long StorageUsed { get; set; } // Bytes
        public long BandwidthUsed { get; set; } // Bytes
        public int APICallsCount { get; set; }
        public long FileOperationCount { get; set; }
        public int ActiveUserCount { get; set; }
        public long StorageTransactions { get; set; } // Total GET/PUT/DELETE 

        public DateTime SnapshotDate { get; set; } // month

        public Tenant Tenant { get; set; }
    }
}