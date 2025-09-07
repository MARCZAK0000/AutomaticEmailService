using App.EmailBuilder.Options;
using App.EmailBuilder.Service;
using Microsoft.Extensions.DependencyInjection;

namespace App.EmailBuilder.Extensions
{
    public static class AddEmailSender
    {
        public static IServiceCollection AddEmailSenderService(this IServiceCollection services, Action<EmailSenderOptions> options)
        {
            //Bind Options  
            EmailSenderOptions senderOptions = new();
            options(senderOptions);

            //Register Services 
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            return services;
        }
    }
}
