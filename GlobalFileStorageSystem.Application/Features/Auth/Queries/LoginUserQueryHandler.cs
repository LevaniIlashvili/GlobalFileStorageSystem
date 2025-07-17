using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Application.Exceptions;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Auth.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginUserQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);
            
            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, query.Password))
            {
                throw new UnauthorizedException("Invalid login credentials");
            }

            return _jwtService.GenerateAccessToken(user);
        }
    }
}
