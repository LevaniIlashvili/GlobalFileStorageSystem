using GlobalFileStorageSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorageSystem.Infrastructure.Persistance.Configurations
{
    public class FileRecordConfiguration : IEntityTypeConfiguration<FileRecord>
    {
        public void Configure(EntityTypeBuilder<FileRecord> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FileName).IsRequired().HasMaxLength(255);
            builder.Property(f => f.ContentType).IsRequired();
            builder.Property(f => f.StoragePath).IsRequired();
            builder.Property(f => f.MD5Hash).IsRequired();
            builder.Property(f => f.SHA256Hash).IsRequired();

            builder.Property(f => f.AccessLevel).HasConversion<string>();

            builder.Property(f => f.Metadata)
                .HasColumnType("jsonb");

            builder.Property(f => f.Tags)
                .HasColumnType("text[]");

            builder.HasIndex(f => new { f.TenantId, f.FileName }).IsUnique();
        }
    }
}
