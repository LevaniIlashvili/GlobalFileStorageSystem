using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Auth.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}
