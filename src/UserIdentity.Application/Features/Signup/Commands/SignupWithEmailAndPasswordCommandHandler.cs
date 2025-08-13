using System;

using MediatR; 
using Microsoft.Extensions.Logging;
using UserIdentity.Application.DTOs;
using UserIdentity.Domain.Entities;
using UserIdentity.Domain.ValueObjects;
using UserIdentity.Application.Interfaces;


namespace UserIdentity.Application.Features.Signup.Commands;

public class SignupWithEmailAndPasswordCommandHandler : IRequestHandler<SignupWithEmailAndPasswordCommand, SignupResponse>
{
    private readonly ILogger<SignupWithEmailAndPasswordCommandHandler> _logger;
    private readonly ISqlGenericRepository _accountRepository;
    private readonly IAuthenticationHelper _authenticationHelper;

    public SignupWithEmailAndPasswordCommandHandler(
        ILogger<SignupWithEmailAndPasswordCommandHandler> logger,
        ISqlGenericRepository accountRepository,
        IAuthenticationHelper authenticationHelper)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _authenticationHelper = authenticationHelper;
    }

    public async Task<SignupResponse> Handle(SignupWithEmailAndPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing signup for email: {Email}", request.Email);

        var existingAccount = await _accountRepository.GetAccountByEmailAsync(request.Email);
        if (existingAccount != null)
        {
            _logger.LogWarning("Email {Email} already exists", request.Email);
            return new SignupResponse
            {
                Success = false,
                Message = "Email already exists"
            };
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserRoles = new List<UserRole> { new UserRole { Role = "User" } }
        };

        var account = new Account
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            HashedPassword = _authenticationHelper.HashPassword(request.Password),
            UserId = user.Id,
            User = user
        };

        await _accountRepository.AddAccountAsync(account);
        await _accountRepository.SaveChangesAsync();

        _logger.LogInformation("Successfully signed up user with email: {Email}", request.Email);

        return new SignupResponse
        {
            UserId = user.Id,
            Email = account.Email,
            Success = true,
            Message = "Signup successful"
        };
    }
}