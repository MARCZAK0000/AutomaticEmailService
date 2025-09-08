
using App.RenderEmail.RenderEmail;
using App.RenderEmail.Repository;
using App.RenderEmail.Strategy;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace App.RenderEmail.Extensions
{
    public static class AddRenderEmail
    {
        public static IServiceCollection AddRenderEmailService(this IServiceCollection services)
        {
            services.AddScoped<HtmlRenderer>();
            services.AddSingleton<IEmailRenderStrategy, EmailRenderStrategy>();
            services.AddScoped<IEmailRenderComponent,EmailRenderComponent>();
            services.AddScoped<RenderEmailBuilder>();
            return services;
        }
    }
}
