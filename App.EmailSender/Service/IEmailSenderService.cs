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
        /// <summary>
        /// Sends an email asynchronously using the specified MIME message.
        /// </summary>
        /// <param name="mimeMessage">The MIME message to be sent. Must not be <see langword="null"/>.</param>
        /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the email
        /// was sent successfully; otherwise, <see langword="false"/>.</returns>
        Task <bool> SendEmailAsync(MimeMessage mimeMessage, CancellationToken token);
    }
}
