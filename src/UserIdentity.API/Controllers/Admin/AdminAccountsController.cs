using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.DTOs.Requests;

namespace UserIdentity.API.Controllers.Admin
{
    [Route("api/admin/accounts")]
    [ApiController]
    [Authorize]
    public class AdminAccountsController : ControllerBase
    {
        // GET: api/admin/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? filter = null)
        {
            // Mock response for now - implement with your repository
            var accounts = new List<AccountDto>
            {
                new AccountDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "testuser",
                    Email = "test@example.com",
                    EmailConfirmation = true,
                    CreatedOn = DateTime.UtcNow.AddDays(-30),
                    RequiredActions = new List<RequiredActionDto>
                    {
                        new RequiredActionDto
                        {
                            AccountId = Guid.NewGuid().ToString(),
                            RequiredActionType = RequiredActionType.ConfrimEmail
                        }
                    },
                    Subscriptions = new List<SubscriptionDto>
                    {
                        new SubscriptionDto
                        {
                            Id = Guid.NewGuid(),
                            SubscriptionType = SubscriptionType.Basic,
                            Status = SubscriptionStatus.Active,
                            Plan = SubscriptionPlan.Monthly,
                            StartDate = DateTime.UtcNow.AddDays(-15),
                            EndDate = DateTime.UtcNow.AddDays(15)
                        }
                    }
                },
                new AccountDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "premiumuser",
                    Email = "premium@example.com",
                    EmailConfirmation = true,
                    CreatedOn = DateTime.UtcNow.AddDays(-60),
                    RequiredActions = new List<RequiredActionDto>(),
                    Subscriptions = new List<SubscriptionDto>
                    {
                        new SubscriptionDto
                        {
                            Id = Guid.NewGuid(),
                            SubscriptionType = SubscriptionType.Premium,
                            Status = SubscriptionStatus.Active,
                            Plan = SubscriptionPlan.Yearly,
                            StartDate = DateTime.UtcNow.AddDays(-30),
                            EndDate = DateTime.UtcNow.AddDays(335)
                        }
                    }
                }
            };

            return Ok(accounts);
        }

        // GET: api/admin/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(string id)
        {
            // Mock response - implement with your repository
            var account = new AccountDto
            {
                Id = id,
                Username = "testuser",
                Email = "test@example.com",
                EmailConfirmation = true,
                CreatedOn = DateTime.UtcNow.AddDays(-30),
                RequiredActions = new List<RequiredActionDto>
                {
                    new RequiredActionDto
                    {
                        AccountId = id,
                        RequiredActionType = RequiredActionType.EnableMfa
                    }
                },
                Subscriptions = new List<SubscriptionDto>
                {
                    new SubscriptionDto
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionType = SubscriptionType.Basic,
                        Status = SubscriptionStatus.Active,
                        Plan = SubscriptionPlan.Monthly,
                        StartDate = DateTime.UtcNow.AddDays(-15),
                        EndDate = DateTime.UtcNow.AddDays(15)
                    }
                }
            };

            return Ok(account);
        }

        // POST: api/admin/accounts/{id}/ban
        [HttpPost("{id}/ban")]
        public async Task<ActionResult> BanAccount(string id)
        {
            // Implement ban logic
            return Ok(new { message = $"Account {id} has been banned successfully." });
        }

        // DELETE: api/admin/accounts/{id}/ban
        [HttpDelete("{id}/ban")]
        public async Task<ActionResult> UnbanAccount(string id)
        {
            // Implement unban logic
            return Ok(new { message = $"Account {id} has been unbanned successfully." });
        }

        // POST: api/admin/accounts/{id}/disconnect
        [HttpPost("{id}/disconnect")]
        public async Task<ActionResult> DisconnectAccount(string id)
        {
            // Implement force disconnect logic
            return Ok(new { message = $"Account {id} has been disconnected successfully." });
        }
    }
}
