using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TalentTrack.Core.Interfaces;
using TalentTrack.Infrastructure.Data;
using TalentTrack.Infrastructure.Email;
using TMG.SharedKernel.Constants;

namespace TalentTrack.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager config, ILogger logger)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString(CoreConstants.DefaultConnection)));

            // Inject UnitOfWork
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            return services;
        }
    }
}
