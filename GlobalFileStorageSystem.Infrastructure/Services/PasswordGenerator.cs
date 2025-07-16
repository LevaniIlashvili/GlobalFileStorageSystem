using GlobalFileStorageSystem.Application.Contracts.Infrastructure;

namespace GlobalFileStorageSystem.Infrastructure.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate(int length = 12)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(valid, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
