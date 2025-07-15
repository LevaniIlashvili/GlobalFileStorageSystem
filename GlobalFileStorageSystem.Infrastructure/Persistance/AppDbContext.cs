using GlobalFileStorageSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorageSystem.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<FileRecord> Files => Set<FileRecord>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UsageRecord> UsageRecords => Set<UsageRecord>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
