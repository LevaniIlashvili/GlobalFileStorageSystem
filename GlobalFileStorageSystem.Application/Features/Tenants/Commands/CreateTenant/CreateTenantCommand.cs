using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Domain.Enums;
using MediatR;

namespace GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<Tenant>
    {
        public string OrganizationName { get; set; }
        public string SubdomainPrefix { get; set; }
        public string DataResidencyRegion { get; set; }
        public BillingPlan BillingPlan { get; set; }
        public ComplianceRequirement ComplianceRequirements { get; set; }
        public EncriptionRequirement EncriptionRequirement { get; set; }
        public string AdminEmail { get; set; }
    }
}
