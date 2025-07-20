using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorageSystem.Infrastructure.Repositories
{
    public class UsageRecordRepository : BaseRepository<UsageRecord>, IUsageRecordRepository
    {
        public UsageRecordRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UsageRecord?> GetLatestByTenantIdAsync(Guid tenantId)
        {
            var usageRecord = await _dbContext.UsageRecords
                .Where(u => u.TenantId == tenantId)
                .OrderByDescending(u => u.SnapshotDate)
                .FirstOrDefaultAsync();

            return usageRecord;
        }
    }
}
