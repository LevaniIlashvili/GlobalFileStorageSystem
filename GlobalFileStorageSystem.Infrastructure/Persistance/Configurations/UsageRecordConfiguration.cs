using GlobalFileStorageSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorageSystem.Infrastructure.Persistance.Configurations
{
    public class UsageRecordConfiguration : IEntityTypeConfiguration<UsageRecord>
    {
        public void Configure(EntityTypeBuilder<UsageRecord> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => new { u.TenantId, u.SnapshotDate }).IsUnique();
        }
    }
}
