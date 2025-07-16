using FluentValidation;

namespace GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage("Organization name is required")
                .MaximumLength(100).WithMessage("Organization name must not exceed 100 characters");

            RuleFor(x => x.SubdomainPrefix)
                .NotEmpty().WithMessage("Subdomain prefix is required")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Subdomain prefix must be lowercase alphanumeric with optional dashes")
                .MaximumLength(50).WithMessage("Subdomain prefix must not exceed 50 characters");

            RuleFor(x => x.DataResidencyRegion)
                .NotEmpty().WithMessage("Data residency region is required")
                .MaximumLength(50).WithMessage("Data residency region must not exceed 50 characters");

            RuleFor(x => x.BillingPlan)
                .IsInEnum().WithMessage("Invalid billing plan selected");

            RuleFor(x => x.ComplianceRequirements)
                .IsInEnum().WithMessage("Invalid compliance requirement specified");

            RuleFor(x => x.EncryptionRequirement)
                .IsInEnum().WithMessage("Invalid encryption requirement specified");

            RuleFor(x => x.AdminEmail)
                .NotEmpty().WithMessage("Admin email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Admin email must not exceed 100 characters");
        }
    }
}
