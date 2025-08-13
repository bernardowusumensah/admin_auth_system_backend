using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;

namespace UserIdentity.API.Controllers.Admin
{
    [Route("api/admin/health")]
    [ApiController]
    [Authorize]
    public class ServicesHealthController : ControllerBase
    {
        private readonly IHealthMonitorService _healthMonitorService;

        public ServicesHealthController(IHealthMonitorService healthMonitorService)
        {
            _healthMonitorService = healthMonitorService;
        }

        // GET: api/admin/health/services
        [HttpGet("services")]
        public async Task<ActionResult<ServiceHealthResponse>> GetServicesHealth()
        {
            var response = await _healthMonitorService.GetAllServicesHealthAsync();
            return Ok(response);
        }

        // GET: api/admin/health/services/{serviceName}
        [HttpGet("services/{serviceName}")]
        public async Task<ActionResult<ServiceHealthDto>> GetServiceHealth(string serviceName)
        {
            var serviceHealth = await _healthMonitorService.GetServiceHealthAsync(serviceName);

            if (serviceHealth == null)
            {
                return NotFound(new { message = $"Service '{serviceName}' not found." });
            }

            return Ok(serviceHealth);
        }

        // GET: api/admin/health/overview
        [HttpGet("overview")]
        public async Task<ActionResult> GetHealthOverview()
        {
            var allServices = await _healthMonitorService.GetAllServicesHealthAsync();
            var totalCount = allServices.Services.Count;
            var healthyCount = allServices.Services.Count(s => s.StatusCode == 200);
            var unavailableCount = totalCount - healthyCount;

            return Ok(new
            {
                totalServices = totalCount,
                healthyServices = healthyCount,
                unavailableServices = unavailableCount,
                overallStatus = unavailableCount == 0 ? "All Systems Operational" : "Some Services Down",
                lastChecked = allServices.LastUpdated
            });
        }
    }
}
