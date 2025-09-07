
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.RenderEmail.Extensions
{
    public static class AddRenderEmail
    {
        public static IServiceCollection AddRenderEmailService(this IServiceCollection services)
        {
            services.AddScoped<HtmlRenderer>();
            services.AddTransient<IRenderEmailService, RenderEmailService>();
            return services;
        }
    }
}
