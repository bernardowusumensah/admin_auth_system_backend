using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Application.Features.Admin.Support.Commands;
using UserIdentity.Application.Features.Admin.Support.Queries;
using UserIdentity.Domain.Entities;

namespace UserIdentity.API.Controllers.Admin;

[ApiController]
[Route("api/admin/support/tickets")]
[Authorize]
public class AdminSupportTicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminSupportTicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<SupportTicketsResultDto>> GetTickets([
        FromQuery] string? search,
        [FromQuery] TicketStatus? status,
        [FromQuery] string? category,
        [FromQuery] string? assignedTo,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetSupportTicketsQuery
        {
            Search = search,
            Status = status,
            Category = category,
            AssignedTo = assignedTo,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupportTicketDto>> GetById(Guid id)
    {
        var ticket = await _mediator.Send(new GetSupportTicketByIdQuery { Id = id });
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTicketStatusRequest body)
    {
        if (body.TicketId != null && body.TicketId != id.ToString()) return BadRequest("Mismatched ticket id");
        var success = await _mediator.Send(new UpdateTicketStatusCommand { Id = id, NewStatus = body.NewStatus });
        if (!success) return NotFound();
        return Ok(new { message = "Ticket status updated successfully" });
    }

    [HttpPost("{id:guid}/notes")]
    public async Task<IActionResult> AddInternalNote(Guid id, [FromBody] AddInternalNoteRequest body)
    {
        if (body.TicketId != null && body.TicketId != id.ToString()) return BadRequest("Mismatched ticket id");
        var success = await _mediator.Send(new AddInternalNoteCommand { TicketId = id, Note = body.NoteContent, Author = User.Identity?.Name ?? "system" });
        if (!success) return NotFound();
        return Ok(new { message = "Internal note added successfully" });
    }

    [HttpPut("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignTicketRequest body)
    {
        var success = await _mediator.Send(new AssignTicketCommand { TicketId = id, AssignedTo = body.AssignedTo });
        if (!success) return NotFound();
        return Ok(new { message = "Ticket assigned successfully" });
    }
}
