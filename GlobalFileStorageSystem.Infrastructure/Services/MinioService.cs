using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace GlobalFileStorageSystem.Infrastructure.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioService> _logger;

        public MinioService(IMinioClient minioClient, ILogger<MinioService> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        private static string GetBucketName(string tenantId) => $"tenant-{tenantId}".ToLowerInvariant();

        public async Task CreateTenantBucketAsync(string tenantId)
        {
            var bucketName = GetBucketName(tenantId);

            var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!exists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                _logger.LogInformation("Bucket '{Bucket}' created for tenant '{TenantId}'", bucketName, tenantId);
            }
        }

        public async Task<string> GeneratePreSignedUploadUrlAsync(
            string tenantId, 
            string objectName, 
            string contentType, 
            TimeSpan expiration)
        {
            var bucketName = GetBucketName(tenantId);

            var args = new PresignedPutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithExpiry((int)expiration.TotalSeconds);

            return await _minioClient.PresignedPutObjectAsync(args);
        }

        public async Task UploadObjectAsync(string tenantId, string objectName, Stream data, string contentType)
        {
            var bucket = GetBucketName(tenantId);

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));

            _logger.LogInformation("Uploaded object '{Object}' to tenant bucket '{Bucket}'", objectName, bucket);
        }

        public async Task<Stream> GetObjectAsync(string tenantId, string objectName)
        {
            var bucket = GetBucketName(tenantId);
            var ms = new MemoryStream();

            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(ms)));

            ms.Position = 0;
            return ms;
        }

        public async Task DeleteObjectAsync(string tenantId, string objectName)
        {
            var bucket = GetBucketName(tenantId);

            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName));

            _logger.LogInformation("Deleted object '{Object}' from tenant bucket '{Bucket}'", objectName, bucket);
        }

        public async Task<bool> BucketExistsAsync(string tenantId)
        {
            var bucket = GetBucketName(tenantId);
            return await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
        }
    }
}
