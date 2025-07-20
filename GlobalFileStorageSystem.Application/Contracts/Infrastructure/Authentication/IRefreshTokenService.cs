namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IRefreshTokenService
    {
        Task StoreRefreshTokenAsync(Guid userId, string refreshToken, DateTime expires);
        Task<Guid?> GetUserIdByRefreshTokenAsync(string refreshToken);
        Task DeleteRefreshTokenAsync(string refreshToken);
    }
}
