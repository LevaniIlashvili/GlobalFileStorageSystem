using GlobalFileStorageSystem.Application.Features.Files.Commands.InitiateFileUpload;
using GlobalFileStorageSystem.Domain.Enums;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Files.Commands.UploadFile
{
    public class InitiateFileUploadCommand : IRequest<InitiateFileUploadResponse>
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
