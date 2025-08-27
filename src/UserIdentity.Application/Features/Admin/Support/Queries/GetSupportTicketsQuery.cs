using MediatR;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Support.Queries;

public class GetSupportTicketsQuery : IRequest<SupportTicketsResultDto>
{
    public string? Search { get; init; }
    public TicketStatus? Status { get; init; }
    public string? Category { get; init; }
    public string? AssignedTo { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class SupportTicketsResultDto
{
    public IEnumerable<SupportTicketDto> Tickets { get; set; } = Enumerable.Empty<SupportTicketDto>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
