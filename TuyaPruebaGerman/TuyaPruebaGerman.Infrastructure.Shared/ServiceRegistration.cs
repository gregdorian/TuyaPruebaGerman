using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Domain.Settings;
using TuyaPruebaGerman.Infrastructure.Shared.Services;

namespace TuyaPruebaGerman.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMockService, MockService>();
        }
    }
}