
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;

namespace UserIdentity.Infrastructure.Services
{
    public class HealthMonitorService : IHealthMonitorService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _serviceEndpoints;

        public HealthMonitorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
            // Configure service endpoints - these would typically come from configuration
            _serviceEndpoints = new Dictionary<string, string>
            {
                { "useridentity", "http://localhost:5001/health" },
                { "player", "http://localhost:5002/health" },
                { "gamesettings", "http://localhost:5003/health" },
                { "orders", "http://localhost:5004/health" }
            };
        }

        public async Task<ServiceHealthResponse> GetAllServicesHealthAsync()
        {
            var services = new List<ServiceHealthDto>();

            foreach (var service in _serviceEndpoints)
            {
                var health = await CheckServiceHealthAsync(service.Key, service.Value);
                services.Add(health);
            }

            return new ServiceHealthResponse
            {
                Services = services,
                LastUpdated = DateTime.UtcNow
            };
        }

        public async Task<ServiceHealthDto?> GetServiceHealthAsync(string serviceName)
        {
            var normalizedName = serviceName.ToLower();
            
            if (!_serviceEndpoints.ContainsKey(normalizedName))
            {
                return null;
            }

            return await CheckServiceHealthAsync(normalizedName, _serviceEndpoints[normalizedName]);
        }

        public async Task<bool> IsServiceHealthyAsync(string serviceName)
        {
            var health = await GetServiceHealthAsync(serviceName);
            return health?.StatusCode == 200;
        }

        private async Task<ServiceHealthDto> CheckServiceHealthAsync(string serviceName, string endpoint)
        {
            try
            {                
                // Simulate some services being down
                var isHealthy = serviceName != "gamesettings"; // GameSettings service is down in our mock
                
                return new ServiceHealthDto
                {
                    Service = FormatServiceName(serviceName),
                    StatusCode = isHealthy ? 200 : 401,
                    Status = isHealthy ? "Healthy" : "Unavailable"
                };
            }
            catch (Exception)
            {
                return new ServiceHealthDto
                {
                    Service = FormatServiceName(serviceName),
                    StatusCode = 500,
                    Status = "Unavailable"
                };
            }
        }

        private static string FormatServiceName(string serviceName)
        {
            return serviceName.ToLower() switch
            {
                "useridentity" => "UserIdentity Service",
                "player" => "Player Service",
                "gamesettings" => "GameSettings Service",
                "orders" => "Orders Service",
                _ => serviceName
            };
        }
    }
}
