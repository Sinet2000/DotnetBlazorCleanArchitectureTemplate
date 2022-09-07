using PaperStop.Application.Requests.Mail;

namespace PaperStop.Application.Interfaces.Services;

public interface IMailService
{
    Task SendAsync(MailRequest request);
}
