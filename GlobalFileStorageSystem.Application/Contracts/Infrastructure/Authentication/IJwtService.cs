using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
    }
}
