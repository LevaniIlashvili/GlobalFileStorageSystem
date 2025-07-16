using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GlobalFileStorageSystem.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;

        public EmailService(IOptions<SmtpOptions> options)
        {
            _smtpOptions = options.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
            {
                Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password),
                EnableSsl = _smtpOptions.EnableSsl,
            };

            var mailMessage = new MailMessage()
            {
                From = new MailAddress(_smtpOptions.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
