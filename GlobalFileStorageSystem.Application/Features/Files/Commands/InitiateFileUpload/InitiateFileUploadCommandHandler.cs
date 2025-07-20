using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Application.Features.Files.Commands.InitiateFileUpload;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Domain.Enums;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Files.Commands.UploadFile
{
    public class InitiateFileUploadCommandHandler : IRequestHandler<InitiateFileUploadCommand, InitiateFileUploadResponse>
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IAsyncRepository<FileRecord> _fileRepository;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<Tenant> _tenantRepository;
        private readonly IUsageRecordRepository _usageRecordRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMinioService _minioService;

        public InitiateFileUploadCommandHandler(
            ILoggedInUserService loggedInUserService,
            IAsyncRepository<FileRecord> fileRepository,
            IAsyncRepository<User> userRepository,
            IAsyncRepository<Tenant> tenantRepository,
            IUsageRecordRepository usageRecordRepository,
            IUnitOfWork unitOfWork,
            IMinioService minioService)
        {
            _loggedInUserService = loggedInUserService;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _usageRecordRepository = usageRecordRepository;
            _unitOfWork = unitOfWork;
            _minioService = minioService;
        }

        public async Task<InitiateFileUploadResponse> Handle(InitiateFileUploadCommand command, CancellationToken cancellationToken)
        {
            var userId = _loggedInUserService.UserId;
            var permissions = _loggedInUserService.Permissions;

            if (!permissions.HasFlag(Permission.FileWrite))
            {
                throw new Exceptions.ForbiddenException("You dont have permission to upload a file");
            }

            var user = (await _userRepository.GetByIdAsync(userId, cancellationToken))!;

            var tenant = await _tenantRepository.GetByIdAsync(user.TenantId, cancellationToken);

            var usageRecord = await _usageRecordRepository.GetLatestByTenantIdAsync(user.TenantId);
            if (tenant.StorageQuota < usageRecord.StorageUsed + command.FileSize)
            {
                throw new Exceptions.ForbiddenException("Storage quota exceeded");
            }

            var objectKey = $"{Guid.NewGuid()}_{command.FileName}";

            var url = await _minioService.GeneratePreSignedUploadUrlAsync(
                user.TenantId.ToString(), objectKey, command.ContentType, TimeSpan.FromMinutes(60));

            var file = new FileRecord()
            {
                FileName = command.FileName,
                ContentType = command.ContentType,
                FileSize = command.FileSize,
                AccessLevel = command.AccessLevel,
                StoragePath = objectKey,
                SHA256Hash = "",
                MD5Hash = "",
                Tags = command.Tags,
                Metadata = command.Metadata,
                TenantId = user.TenantId,
                UploadedBy = user.Id,
                UploadTimestamp = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                VersionNumber = 1
            };

            await _fileRepository.AddAsync(file);

            await _unitOfWork.SaveChangesAsync();

            return new InitiateFileUploadResponse
            {
                UploadUrl = url,
                ObjectKey = objectKey,
                FileId = file.Id
            };
        }
    }
}
