using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using GlobalFileStorageSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GlobalFileStorageSystem.Infrastructure.Services.Authentication
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new Application.Exceptions.UnauthorizedException("User not authenticated");
                }

                if (!Guid.TryParse(userId, out Guid result))
                {
                    throw new Application.Exceptions.UnauthorizedException("Invalid user id");
                }

                return result;
            }
        }

        public string Role
        {
            get
            {
                var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrWhiteSpace(role))
                {
                    throw new Application.Exceptions.UnauthorizedException("User not authenticated");
                }

                return role;
            }
        }

        public Permission Permissions
        {
            get
            {
                var permissions = _httpContextAccessor.HttpContext?.User.FindFirst("permissions")?.Value;

                if (string.IsNullOrWhiteSpace(permissions))
                {
                    throw new Application.Exceptions.UnauthorizedException("User not authenticated");
                }

                if (!Enum.TryParse(permissions, out Permission result))
                {
                    throw new Application.Exceptions.UnauthorizedException("Invalid permissions");
                }

                return result;
            }
        }
    }
}
