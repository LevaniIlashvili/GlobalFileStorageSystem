using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Auth.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<RefreshTokenResponse>
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
