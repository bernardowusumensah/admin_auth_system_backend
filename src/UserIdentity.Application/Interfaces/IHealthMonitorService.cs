using UserIdentity.Application.DTOs.Admin;

namespace UserIdentity.Application.Interfaces
{
    public interface IHealthMonitorService
    {
        /// <summary>
        /// Gets the health status of all monitored services
        /// </summary>
        /// <returns>A response containing all services and their health status</returns>
        Task<ServiceHealthResponse> GetAllServicesHealthAsync();

        /// <summary>
        /// Gets the health status of a specific service by name
        /// </summary>
        /// <param name="serviceName">The name of the service to check</param>
        /// <returns>The health status of the specified service, or null if not found</returns>
        Task<ServiceHealthDto?> GetServiceHealthAsync(string serviceName);

        /// <summary>
        /// Checks if a specific service is healthy (status code 200)
        /// </summary>
        /// <param name="serviceName">The name of the service to check</param>
        /// <returns>True if the service is healthy, false otherwise</returns>
        Task<bool> IsServiceHealthyAsync(string serviceName);
    }
}