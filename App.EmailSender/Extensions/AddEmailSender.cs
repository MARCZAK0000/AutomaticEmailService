using App.EmailBuilder.Options;
using App.EmailBuilder.Service;
using Microsoft.Extensions.DependencyInjection;

namespace App.EmailBuilder.Extensions
{
    public static class AddEmailSender
    {
        /// <summary>
        /// Adds the email sender service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>This method registers the <see cref="IEmailSenderService"/> implementation as a
        /// transient service. The provided <paramref name="options"/> delegate is used to configure the email sender
        /// options.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the email sender service will be added.</param>
        /// <param name="options">A delegate to configure the <see cref="EmailSenderOptions"/> used by the email sender service.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
        public static IServiceCollection AddEmailSenderService(this IServiceCollection services, Action<EmailSenderOptions> options)
        {
            //Bind Options  
            EmailSenderOptions senderOptions = new();
            options(senderOptions);

            //Register Services 
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddSingleton(senderOptions);
            return services;
        }
    }
}
