using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        Task<string> GenerateAndStoreRefreshTokenAsync(Guid userId);
        Task<Guid?> GetUserIdByRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
