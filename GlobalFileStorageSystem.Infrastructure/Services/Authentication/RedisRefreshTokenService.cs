using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace GlobalFileStorageSystem.Infrastructure.Services.Authentication
{
    public class RedisRefreshTokenService : IRefreshTokenService
    {
        private readonly StackExchange.Redis.IDatabase _database;
        private const string RefreshTokenPrefix = "refresh_token:";

        public RedisRefreshTokenService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task StoreRefreshTokenAsync(Guid userId, string refreshToken, DateTime expires)
        {
            var key = RefreshTokenPrefix + userId;
            var expiry = expires - DateTime.UtcNow;

            await _database.StringSetAsync(key, refreshToken, expiry);
        }

        public async Task<string?> GetRefreshTokenAsync(Guid userId)
        {
            var key = RefreshTokenPrefix + userId;
            return await _database.StringGetAsync(key);
        }

        public async Task DeleteRefreshTokenAsync(Guid userId)
        {
            var key = RefreshTokenPrefix + userId;
            await _database.KeyDeleteAsync(key);
        }
    }
}
