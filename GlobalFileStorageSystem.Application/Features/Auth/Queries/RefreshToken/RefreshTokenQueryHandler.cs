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
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new UnauthorizedException("Invalid user");

            var valid = await _jwtService.ValidateRefreshTokenAsync(user.Id, request.RefreshToken);
            if (!valid)
                throw new UnauthorizedException("Invalid refresh token");

            await _jwtService.RevokeRefreshTokenAsync(user.Id);

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
