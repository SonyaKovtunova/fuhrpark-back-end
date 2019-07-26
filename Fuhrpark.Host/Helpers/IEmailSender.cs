using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailAddress recipient, string subject, string message);
    }
}
