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

        public CreateTenantCommandHandler(
            ITenantRepository tennantRepository,
            IUserRepository userRepository)
        {
            _tenantRepository = tennantRepository;
            _userRepository = userRepository;
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

            var addedTenant = await _tenantRepository.AddAsync(tenant);

            var user = new User()
            {
                TenantId = addedTenant.Id,
                MFAEnabled = true,
                Role = UserRole.TenantAdmin,
                Permissions = Permission.All,
                Email = command.AdminEmail,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return _mapper.Map<TenantViewmodel>(addedTenant);
        }
    }
}