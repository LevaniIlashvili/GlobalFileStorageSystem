namespace GlobalFileStorageSystem.Application.Features.Files.Commands.InitiateFileUpload
{
    public class InitiateFileUploadResponse
    {
        public string UploadUrl { get; set; }
        public string ObjectKey { get; set; }
        public Guid FileId { get; set; }
    }
}
