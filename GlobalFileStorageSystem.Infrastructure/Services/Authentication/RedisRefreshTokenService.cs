using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore.Storage;
using Minio.DataModel;
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
            var key = RefreshTokenPrefix + refreshToken;
            var expiry = expires - DateTime.UtcNow;

            await _database.StringSetAsync(key, userId.ToString(), expiry);
        }

        public async Task<Guid?> GetUserIdByRefreshTokenAsync(string refreshToken)
        {
            var key = RefreshTokenPrefix + refreshToken;
            var value = await _database.StringGetAsync(key);

            return value.HasValue && Guid.TryParse(value, out var userId) ? userId : null;
        }

        public async Task DeleteRefreshTokenAsync(string refreshToken)
        {
            var key = RefreshTokenPrefix + refreshToken;
            await _database.KeyDeleteAsync(key);
        }
    }
}
