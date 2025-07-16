using AutoMapper;
using GlobalFileStorageSystem.Application.Features.Tenants.Commands.CreateTenant;
using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tenant, TenantViewmodel>().ReverseMap();
        }
    }
}
