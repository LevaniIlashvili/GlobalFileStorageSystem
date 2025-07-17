using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
