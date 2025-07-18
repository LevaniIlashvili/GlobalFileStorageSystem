using GlobalFileStorageSystem.Domain.Entities;

namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
