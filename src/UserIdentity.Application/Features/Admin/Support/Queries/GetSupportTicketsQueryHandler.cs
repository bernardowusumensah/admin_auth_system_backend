using MediatR;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;
using UserIdentity.Application.Mapping;

namespace UserIdentity.Application.Features.Admin.Support.Queries;

public class GetSupportTicketsQueryHandler : IRequestHandler<GetSupportTicketsQuery, SupportTicketsResultDto>
{
    private readonly ISqlGenericRepository _repository;

    public GetSupportTicketsQueryHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<SupportTicketsResultDto> Handle(GetSupportTicketsQuery request, CancellationToken cancellationToken)
    {
        var (tickets, total) = await _repository.GetSupportTicketsAsync(
            request.Search,
            request.Status,
            request.Category,
            request.AssignedTo,
            request.FromDate,
            request.ToDate,
            request.Page <= 0 ? 1 : request.Page,
            request.PageSize <= 0 ? 20 : request.PageSize);

        return new SupportTicketsResultDto
        {
            Tickets = tickets.Select(t => t.ToDto()),
            TotalCount = total,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
