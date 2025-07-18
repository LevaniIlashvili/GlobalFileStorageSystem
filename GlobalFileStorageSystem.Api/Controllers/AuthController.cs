using GlobalFileStorageSystem.Application.Features.Auth.Queries.LoginUser;
using GlobalFileStorageSystem.Application.Features.Auth.Queries.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserQuery query)
        {
            var accessToken = await _mediator.Send(query);
            return Ok(accessToken);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
