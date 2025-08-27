using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Application.Features.Support.Tickets.Commands;

namespace UserIdentity.API.Controllers.Support;

[ApiController]
[Route("api/support/tickets")]
[AllowAnonymous] // allow public submission; adjust as needed
public class SupportTicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupportTicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class CreateSupportTicketRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Category { get; set; }
        public string? Subject { get; set; }
        public string? Details { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<SupportTicketDto>> Create([FromBody] CreateSupportTicketRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Username))
        {
            return BadRequest("Either email or username must be provided");
        }
        var cmd = new CreateSupportTicketCommand
        {
            Username = request.Username,
            Email = request.Email,
            Category = request.Category,
            Subject = request.Subject,
            Details = request.Details
        };
        var dto = await _mediator.Send(cmd);
        return CreatedAtAction(null, new { id = dto.TicketId }, dto); // client can GET via admin endpoint
    }
}
