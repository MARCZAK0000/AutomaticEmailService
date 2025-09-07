using App.EmailBuilder.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net;

namespace App.EmailBuilder.Service
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly EmailSenderOptions _emailSenderOptions;

        public EmailSenderService(ILogger<EmailSenderService> logger, EmailSenderOptions emailSenderOptions)
        {
            _logger = logger;
            _emailSenderOptions = emailSenderOptions;
        }

        public async Task<bool> SendEmailAsync(MimeMessage mimeMessage, CancellationToken token)
        {
            try
            {
                using var smtp = new SmtpClient();
                _logger.LogInformation("{Host}, {Date} : Email Sending to: {addressTo}",nameof(EmailSenderService), DateTime.Now, mimeMessage.To);
                await smtp.ConnectAsync(_emailSenderOptions.SmptHost, _emailSenderOptions.Port, MailKit.Security.SecureSocketOptions.StartTls, token);
                await smtp.AuthenticateAsync(new NetworkCredential(_emailSenderOptions.EmailName, _emailSenderOptions.Password), token);
                await smtp.SendAsync(mimeMessage, token);
                await smtp.DisconnectAsync(true, token);
                _logger.LogInformation("{Host}, {Date} : Email Send to: {addressTo}", nameof(EmailSenderService), DateTime.Now, mimeMessage.To);
                return true;
            }
            catch (Exception)
            {
                _logger.LogError("{Host}, {Date} : Email Not Send to: {addressTo}", nameof(EmailSenderService), DateTime.Now, mimeMessage.To);
                throw;
            }
        }
    }
}
