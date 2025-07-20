using GlobalFileStorageSystem.Application.Features.Files.Commands.InitiateFileUpload;
using GlobalFileStorageSystem.Application.Features.Files.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ISender _sender;

        public FileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<InitiateFileUploadResponse>> InitiateFileUpload([FromBody] InitiateFileUploadCommand command)
        {
            var result = await _sender.Send(command);

            return CreatedAtAction(null, result);
        }
    }
}
