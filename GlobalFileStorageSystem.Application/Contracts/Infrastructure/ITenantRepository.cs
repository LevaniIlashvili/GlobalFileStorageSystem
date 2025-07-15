using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface ITenantRepository : IAsyncRepository<Tenant>
    {
    }
}
