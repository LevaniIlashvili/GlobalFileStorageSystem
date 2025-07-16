using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
