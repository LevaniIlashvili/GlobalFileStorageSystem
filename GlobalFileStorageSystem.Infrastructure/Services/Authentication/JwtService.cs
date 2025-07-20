using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GlobalFileStorageSystem.Infrastructure.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtService(IOptions<JwtSettings> jwtSettings, IRefreshTokenService refreshTokenService)
        {
            _jwtSettings = jwtSettings.Value;
            _refreshTokenService = refreshTokenService;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("permissions", user.Permissions.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateAndStoreRefreshTokenAsync(Guid userId)
        {
            var refreshToken = GenerateSecureRefreshToken();
            var expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            await _refreshTokenService.StoreRefreshTokenAsync(userId, refreshToken, expires);

            return refreshToken;
        }

        private static string GenerateSecureRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<Guid?> GetUserIdByRefreshTokenAsync(string refreshToken)
        {
            return await _refreshTokenService.GetUserIdByRefreshTokenAsync(refreshToken);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            await _refreshTokenService.DeleteRefreshTokenAsync(refreshToken);
        }
    }
}
