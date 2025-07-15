using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorageSystem.Infrastructure.Persistance.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.OrganizationName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.SubdomainPrefix).IsRequired().HasMaxLength(50);
            builder.HasIndex(t => t.SubdomainPrefix).IsUnique();

            builder.Property(t => t.TenantStatus).HasConversion<string>();
            builder.Property(t => t.BillingPlan).HasConversion<string>();
            builder.Property(t => t.EncryptionRequirements).HasConversion<string>();

            builder.HasMany(t => t.Users)
                   .WithOne(u => u.Tenant)
                   .HasForeignKey(u => u.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Files)
                   .WithOne(f => f.Tenant)
                   .HasForeignKey(f => f.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Usages)
                   .WithOne(u => u.Tenant)
                   .HasForeignKey(u => u.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.DataResidencyRegion).HasMaxLength(50);
        }
    }
}