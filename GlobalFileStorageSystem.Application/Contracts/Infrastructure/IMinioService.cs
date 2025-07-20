namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface IMinioService
    {
        Task CreateTenantBucketAsync(string tenantId);
        Task<string> GeneratePreSignedUploadUrlAsync(
            string tenantId,
            string objectName,
            string contentType,
            TimeSpan expiration);
        Task UploadObjectAsync(string tenantId, string objectName, Stream data, string contentType);
        Task<Stream> GetObjectAsync(string tenantId, string objectName);
        Task DeleteObjectAsync(string tenantId, string objectName);
        Task<bool> BucketExistsAsync(string tenantId);
    }
}
