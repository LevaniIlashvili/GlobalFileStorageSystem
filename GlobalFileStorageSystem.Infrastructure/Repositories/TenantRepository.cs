using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Infrastructure.Persistance;

namespace GlobalFileStorageSystem.Infrastructure.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
