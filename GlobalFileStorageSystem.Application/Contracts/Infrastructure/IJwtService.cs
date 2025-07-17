using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
    }
}
