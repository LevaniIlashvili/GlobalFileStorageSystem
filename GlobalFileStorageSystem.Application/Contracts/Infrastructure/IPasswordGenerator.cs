namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface IPasswordGenerator
    {
        string Generate(int length = 12);
    }
}
