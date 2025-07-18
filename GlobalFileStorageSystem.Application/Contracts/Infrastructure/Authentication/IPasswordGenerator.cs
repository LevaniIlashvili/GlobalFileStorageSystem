namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication
{
    public interface IPasswordGenerator
    {
        string Generate(int length = 12);
    }
}
