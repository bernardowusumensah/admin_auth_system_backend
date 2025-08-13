using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Application.Interfaces;
using UserIdentity.Infrastructure.Repositories;
using UserIdentity.Infrastructure.Services;

namespace UserIdentity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<ISqlGenericRepository, SqlGenericRepository>();

            // Register services
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IHealthMonitorService, HealthMonitorService>();
            services.AddScoped<IAuthService, AuthService>();

            // Register HttpClient for health monitoring
            services.AddHttpClient<HealthMonitorService>();


            return services;
        }
    }
}


