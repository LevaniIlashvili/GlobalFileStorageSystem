using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories
{
    public interface IUsageRecordRepository : IAsyncRepository<UsageRecord>
    {
        Task<UsageRecord?> GetLatestByTenantIdAsync(Guid tenantId);
    }
}
