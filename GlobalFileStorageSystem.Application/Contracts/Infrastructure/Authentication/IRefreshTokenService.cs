namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IRefreshTokenService
    {
        Task StoreRefreshTokenAsync(Guid userId, string refreshToken, DateTime expires);
        Task<string?> GetRefreshTokenAsync(Guid userId);
        Task DeleteRefreshTokenAsync(Guid userId);
    }
}
