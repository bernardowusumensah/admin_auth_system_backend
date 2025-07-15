using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Application.Interfaces;
using UserIdentity.Infrastructure.Services;

namespace UserIdentity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }
    }
}
