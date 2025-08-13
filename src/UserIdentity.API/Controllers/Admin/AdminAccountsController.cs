using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.DTOs.Requests;
using UserIdentity.Application.Features.Admin.Accounts.Commands;
using UserIdentity.Application.Features.Admin.Accounts.Queries;
using UserIdentity.Domain.Entities;

namespace UserIdentity.API.Controllers.Admin
{
    [Route("api/admin/accounts")]
    [ApiController]
    [Authorize]
    public class AdminAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/admin/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? filter = null)
        {
            var query = new GetAllAccountsQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                Filter = filter
            };

            var accounts = await _mediator.Send(query);
            return Ok(accounts);
        }

        // GET: api/admin/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(string id)
        {
            if (!Guid.TryParse(id, out var accountId))
                return BadRequest("Invalid account ID format");

            var query = new GetAccountByIdQuery { AccountId = accountId };
            var account = await _mediator.Send(query);

            if (account == null)
                return NotFound($"Account with ID {id} not found");

            return Ok(account);
        }

        // POST: api/admin/accounts/{id}/ban
        [HttpPost("{id}/ban")]
        public async Task<ActionResult> BanAccount(string id)
        {
            if (!Guid.TryParse(id, out var accountId))
                return BadRequest("Invalid account ID format");

            var command = new BanAccountCommand { AccountId = accountId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Account with ID {id} not found");

            return Ok(new { message = $"Account {id} has been banned successfully." });
        }

        // DELETE: api/admin/accounts/{id}/ban
        [HttpDelete("{id}/ban")]
        public async Task<ActionResult> UnbanAccount(string id)
        {
            if (!Guid.TryParse(id, out var accountId))
                return BadRequest("Invalid account ID format");

            var command = new UnbanAccountCommand { AccountId = accountId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Account with ID {id} not found");

            return Ok(new { message = $"Account {id} has been unbanned successfully." });
        }

        // POST: api/admin/accounts/{id}/disconnect
        [HttpPost("{id}/disconnect")]
        public async Task<ActionResult> DisconnectAccount(string id)
        {
            if (!Guid.TryParse(id, out var accountId))
                return BadRequest("Invalid account ID format");

            var command = new DisconnectAccountCommand { AccountId = accountId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Account with ID {id} not found");

            return Ok(new { message = $"Account {id} has been disconnected successfully." });
        }
    }
}
