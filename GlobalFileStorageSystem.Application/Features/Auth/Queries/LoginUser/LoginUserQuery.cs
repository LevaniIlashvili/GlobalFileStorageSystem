using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Auth.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
