using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.DTOs.Requests;

namespace UserIdentity.API.Controllers.Admin
{
    [Route("api/admin/subscriptions")]
    [ApiController]
    [Authorize]
    public class AdminSubscriptionsController : ControllerBase
    {
        // POST: api/admin/subscriptions
        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            // Mock response - implement with your repository/service
            var subscription = new SubscriptionDto
            {
                Id = Guid.NewGuid(),
                SubscriptionType = request.SubscriptionType,
                Status = SubscriptionStatus.Active,
                Plan = request.SubscriptionPlan,
                StartDate = DateTime.UtcNow,
                EndDate = request.SubscriptionPlan switch
                {
                    SubscriptionPlan.Monthly => DateTime.UtcNow.AddMonths(1),
                    SubscriptionPlan.Yearly => DateTime.UtcNow.AddYears(1),
                    SubscriptionPlan.Lifetime => DateTime.MaxValue,
                    _ => DateTime.UtcNow.AddMonths(1)
                }
            };

            return Ok(new { 
                message = "Subscription created successfully", 
                subscription = subscription 
            });
        }

        // DELETE: api/admin/subscriptions/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelSubscription(Guid id)
        {
            // Implement subscription cancellation logic
            return Ok(new { message = $"Subscription {id} has been cancelled successfully." });
        }

        // POST: api/admin/subscriptions/cancel
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelSubscriptionByRequest([FromBody] CancelSubscriptionRequest request)
        {
            // Implement subscription cancellation logic
            return Ok(new { message = $"Subscription {request.SubscriptionId} has been cancelled successfully." });
        }

        // GET: api/admin/subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetSubscriptions()
        {
            // Mock response - implement with your repository
            var subscriptions = new List<SubscriptionDto>
            {
                new SubscriptionDto
                {
                    Id = Guid.NewGuid(),
                    SubscriptionType = SubscriptionType.Basic,
                    Status = SubscriptionStatus.Active,
                    Plan = SubscriptionPlan.Monthly,
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    EndDate = DateTime.UtcNow.AddDays(15)
                },
                new SubscriptionDto
                {
                    Id = Guid.NewGuid(),
                    SubscriptionType = SubscriptionType.Premium,
                    Status = SubscriptionStatus.Active,
                    Plan = SubscriptionPlan.Yearly,
                    StartDate = DateTime.UtcNow.AddDays(-30),
                    EndDate = DateTime.UtcNow.AddDays(335)
                }
            };

            return Ok(subscriptions);
        }
    }
}
