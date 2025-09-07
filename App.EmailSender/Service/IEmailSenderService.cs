using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EmailBuilder.Service
{
    public interface IEmailSenderService
    {
        Task <bool> SendEmailAsync(MimeMessage mimeMessage, CancellationToken token);
    }
}
