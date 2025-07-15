using GlobalFileStorageSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorageSystem.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(u => u.Email);

            builder.Property(u => u.Role).HasConversion<string>();

            builder.Property(u => u.IPWhitelist)
                .HasColumnType("text[]");

            builder.Property(u => u.SessionTimeout).HasConversion(
                v => v.Ticks,
                v => TimeSpan.FromTicks(v)
            );

            builder.HasMany(u => u.Files)
                   .WithOne(f => f.User)
                   .HasForeignKey(f => f.UploadedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
