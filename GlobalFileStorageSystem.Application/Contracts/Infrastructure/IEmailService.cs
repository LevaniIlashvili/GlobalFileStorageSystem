namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string body);
    }
}
