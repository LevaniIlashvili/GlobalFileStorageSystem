using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        Task<string> GenerateAndStoreRefreshTokenAsync(Guid userId);
        Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
        Task RevokeRefreshTokenAsync(Guid userId);
    }
}
