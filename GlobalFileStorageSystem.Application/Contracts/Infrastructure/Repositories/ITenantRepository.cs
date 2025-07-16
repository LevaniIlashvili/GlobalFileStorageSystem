using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories
{
    public interface ITenantRepository : IAsyncRepository<Tenant>
    {
    }
}
