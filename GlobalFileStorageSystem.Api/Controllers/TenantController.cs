using GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant;
using GlobalFileStorageSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantCommand command)
        {
            var tenant = await _mediator.Send(command);

            return Ok(tenant);
        }
    }
}
