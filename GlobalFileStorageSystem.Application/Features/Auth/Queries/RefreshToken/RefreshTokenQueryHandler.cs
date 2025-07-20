using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Application.Exceptions;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Auth.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenResponse>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public RefreshTokenQueryHandler(
            IJwtService jwtService,
            IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var userId = await _jwtService.GetUserIdByRefreshTokenAsync(request.RefreshToken);
            if (userId == null)
                throw new UnauthorizedException("Invalid refresh token");

            var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
            if (user == null)
                throw new UnauthorizedException("User not found");

            await _jwtService.RevokeRefreshTokenAsync(request.RefreshToken);

            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = await _jwtService.GenerateAndStoreRefreshTokenAsync(user.Id);

            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
