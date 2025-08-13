using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.DTOs.Requests;
using UserIdentity.Application.Features.Admin.Subscriptions.Commands;
using UserIdentity.Application.Features.Admin.Subscriptions.Queries;

namespace UserIdentity.API.Controllers.Admin
{
    [Route("api/admin/subscriptions")]
    [ApiController]
    [Authorize]
    public class AdminSubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminSubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST: api/admin/subscriptions
        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            var command = new CreateSubscriptionCommand
            {
                AccountId = request.AccountId,
                SubscriptionType = request.SubscriptionType,
                SubscriptionPlan = request.SubscriptionPlan
            };

            var subscription = await _mediator.Send(command);

            return Ok(new { 
                message = "Subscription created successfully", 
                subscription = subscription 
            });
        }

        // DELETE: api/admin/subscriptions/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelSubscription(Guid id)
        {
            var command = new CancelSubscriptionCommand { SubscriptionId = id };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound(new { message = $"Subscription {id} not found." });

            return Ok(new { message = $"Subscription {id} has been cancelled successfully." });
        }

        // POST: api/admin/subscriptions/cancel
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelSubscriptionByRequest([FromBody] CancelSubscriptionRequest request)
        {
            var command = new CancelSubscriptionCommand { SubscriptionId = request.SubscriptionId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound(new { message = $"Subscription {request.SubscriptionId} not found." });

            return Ok(new { message = $"Subscription {request.SubscriptionId} has been cancelled successfully." });
        }

        // GET: api/admin/subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetSubscriptions()
        {
            var query = new GetAllSubscriptionsQuery();
            var subscriptions = await _mediator.Send(query);

            return Ok(subscriptions);
        }
    }
}
