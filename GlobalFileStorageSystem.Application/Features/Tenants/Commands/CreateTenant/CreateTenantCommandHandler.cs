using AutoMapper;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Domain.Enums;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, TenantViewmodel>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMinioService _minioService;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTenantCommandHandler(
            ITenantRepository tenantRepository,
            IUserRepository userRepository,
            IMinioService minioService,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher)
        {
            _tenantRepository = tenantRepository;
            _userRepository = userRepository;
            _minioService = minioService;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<TenantViewmodel> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
        {
            long storageQuota;
            long bandwidthQuota;
            int apiRateLimit;

            switch (command.BillingPlan)
            {
                case BillingPlan.Starter:
                    storageQuota = 10L * 1024L * 1024L * 1024L;       // 10 GB
                    bandwidthQuota = 50L * 1024L * 1024L * 1024L;    // 50 GB
                    apiRateLimit = 1000;                              // 1,000 req/min
                    break;

                case BillingPlan.Professional:
                    storageQuota = 1L * 1024L * 1024L * 1024L * 1024L;  // 1 TB
                    bandwidthQuota = 500L * 1024L * 1024L * 1024L;       // 500 GB
                    apiRateLimit = 10_000;                                // 10K req/min
                    break;

                case BillingPlan.Enterprise:
                    storageQuota = 10L * 1024L * 1024L * 1024L * 1024L;  // 10 TB
                    bandwidthQuota = 2L * 1024L * 1024L * 1024L * 1024L; // 2 TB
                    apiRateLimit = 100_000;                               // 100K req/min
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(command.BillingPlan), "Unknown billing plan");
            }

            var tenant = new Tenant()
            {
                OrganizationName = command.OrganizationName,
                SubdomainPrefix = command.SubdomainPrefix,
                DataResidencyRegion = command.DataResidencyRegion,
                APIRateLimit = apiRateLimit,
                BandwithQuota = bandwidthQuota,
                StorageQuota = storageQuota,
                BillingPlan = command.BillingPlan,
                TenantStatus = TenantStatus.Active,
                ComplianceRequirements = command.ComplianceRequirements,
                EncryptionRequirements = command.EncryptionRequirement,
                CreatedAt = DateTime.UtcNow,
            };

            await _tenantRepository.AddAsync(tenant);

            var generatedPassword = _passwordGenerator.Generate();
            var user = new User()
            {
                TenantId = tenant.Id,
                MFAEnabled = true,
                Role = UserRole.TenantAdmin,
                Permissions = Permission.All,
                Email = command.AdminEmail,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = _passwordHasher.HashPassword(generatedPassword)
            };

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendAsync(
                command.AdminEmail,
                "Welcome to Global File Storage",
                $@"
                    <h2>Temporary Credentials</h2>
                    <p>Hello, your tenant has been created successfully.</p>
                    <p><b>Tenant:</b> {tenant.OrganizationName}</p>
                    <p><b>Admin Email:</b> {command.AdminEmail}</p>
                    <p><b>Temporary Password: {generatedPassword}</p>
                    <p><i>Note: Update your password upon first login.</i></p>"
            );

            await _minioService.CreateTenantBucketAsync(tenant.Id.ToString());

            return _mapper.Map<TenantViewmodel>(tenant);
        }
    }
}