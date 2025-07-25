using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Infrastructure.Interfaces;
using UserIdentity.Infrastructure.Repositories;
using UserIdentity.Infrastructure.Services;

namespace UserIdentity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<ISqlGenericRepository, SqlGenericRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            return services;
        }
    }
}


