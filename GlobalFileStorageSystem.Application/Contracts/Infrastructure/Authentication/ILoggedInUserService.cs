using GlobalFileStorageSystem.Domain.Enums;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface ILoggedInUserService
    {
        public Guid UserId { get; }
        public string Role { get; }
        public Permission Permissions { get; }
    }
}
