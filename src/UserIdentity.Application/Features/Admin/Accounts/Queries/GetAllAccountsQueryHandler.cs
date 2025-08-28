using MediatR;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Accounts.Queries
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, AccountsResponse>
    {
        private readonly ISqlGenericRepository _repository;

        public GetAllAccountsQueryHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountsResponse> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAllAsync<Account>(
                filter: a => string.IsNullOrEmpty(request.Search) ||
                            a.Email.Contains(request.Search) ||
                            (a.Username != null && a.Username.Contains(request.Search)),
                include: q => q.Include(a => a.User)
                              .Include(a => a.RequiredActions)
                              .Include(a => a.Subscriptions),
                orderBy: q => q.OrderByDescending(a => a.CreatedOn)
            );

            // Apply pagination
            var paginatedAccounts = accounts
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var acc = paginatedAccounts.Select(a => new AccountDto
            {
                Id = a.Id.ToString(),
                Username = a.Username,
                Email = a.Email,
                EmailConfirmation = a.EmailConfirmation,
                LockedOut = a.LockedOut,
                CreatedOn = a.CreatedOn,
                UserId = a.UserId.ToString(),
                RequiredActions = a.RequiredActions?.Select(ra => new RequiredActionDto
                {
                    AccountId = ra.AccountId.ToString(),
                    RequiredActionType = ra.RequiredActionType
                }).ToList() ?? new List<RequiredActionDto>(),
                Subscriptions = a.Subscriptions?.Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    SubscriptionType = s.SubscriptionType,
                    Status = s.Status,
                    Plan = s.Plan,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    AccountId = s.AccountId,
                    AccountEmail = a.Email,
                    UserName = a.User != null ? $"{a.User.FirstName} {a.User.LastName}".Trim() : "N/A"
                }).ToList() ?? new List<SubscriptionDto>()
            }).ToList();
            
            return new AccountsResponse
            {
                Accounts = acc,
                TotalCount = accounts.Count(),
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(accounts.Count() / (double)request.PageSize)
            };
        }
    }
}
