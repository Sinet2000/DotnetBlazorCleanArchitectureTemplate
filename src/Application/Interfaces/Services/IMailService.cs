using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Requests.Mail;

namespace FPAAgentura.Application.Interfaces.Services;

public interface IMailService
{
    Task SendAsync(MailRequest request);
}
