using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PaperStop.Application.Configurations;
using PaperStop.Application.Interfaces.Services;
using PaperStop.Application.Requests.Mail;

namespace PaperStop.Shared.Infrastructure.Services;

public class SmtpMailService : IMailService
{
    private readonly MailConfiguration _config;
    private readonly ILogger<SmtpMailService> _logger;

    public SmtpMailService(IOptions<MailConfiguration> config, ILogger<SmtpMailService> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public async Task SendAsync(MailRequest request)
    {
        try
        {
            var email = new MimeMessage
            {
                Sender = new MailboxAddress(_config.DisplayName, request.From ?? _config.From),
                Subject = request.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = request.Body
                }.ToMessageBody()
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.UserName, _config.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }
}
