using MediatR;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Accounts.Queries
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDtoResponse?>
    {
        private readonly ISqlGenericRepository _repository;

        public GetAccountByIdQueryHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountDtoResponse?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAllAsync<Account>(
                filter: a => a.Id == request.AccountId,
                include: q => q.Include(a => a.User)
                              .ThenInclude(u => u.Address)
                              .Include(a => a.RequiredActions)
                              .Include(a => a.Subscriptions)
            );

            var account = accounts.FirstOrDefault();
            if (account == null)
                return null;

            return new AccountDtoResponse
            {
                Id = account.Id.ToString(),
                Username = account.Username,
                Email = account.Email,
                EmailConfirmation = account.EmailConfirmation,
                LockedOut = account.LockedOut,
                CreatedOn = account.CreatedOn,
                UserId = account.UserId.ToString(),
                RequiredActions = account.RequiredActions?.Select(ra => new RequiredActionDto
                {
                    AccountId = ra.AccountId.ToString(),
                    RequiredActionType = ra.RequiredActionType
                }).ToList() ?? new List<RequiredActionDto>(),
                Subscriptions = account.Subscriptions?.Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    SubscriptionType = s.SubscriptionType,
                    Status = s.Status,
                    Plan = s.Plan,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    AccountId = s.AccountId,
                    AccountEmail = account.Email,
                    UserName = account.User != null ? $"{account.User.FirstName} {account.User.LastName}".Trim() : "N/A"
                }
                ).ToList() ?? new List<SubscriptionDto>(),
                User = new UserDto
                {
                    UserId = account.User?.Id.ToString(),
                    DateOfBirth = account.User?.DateOfBirth,
                    DisplayName = account.User?.FirstName + " " + account.User?.LastName,
                }
            };
        }
    }
}
