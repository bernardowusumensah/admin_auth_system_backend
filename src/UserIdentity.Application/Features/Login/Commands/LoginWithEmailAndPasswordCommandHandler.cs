using System;
using MediatR;
using Microsoft.Extensions.Logging;
using UserIdentity.Application.DTOs;
using UserIdentity.Application.Interfaces;

namespace UserIdentity.Application.Features.Login.Commands;

public class LoginWithEmailAndPasswordCommandHandler : IRequestHandler<LoginWithEmailAndPasswordCommand, AuthenticationResponse>
{
    private readonly ILogger<LoginWithEmailAndPasswordCommandHandler> _logger;
    private readonly ISqlGenericRepository _accountRepository;
    private readonly IAuthenticationHelper _authenticationHelper;

    public LoginWithEmailAndPasswordCommandHandler(
        ILogger<LoginWithEmailAndPasswordCommandHandler> logger,
        ISqlGenericRepository accountRepository,
        IAuthenticationHelper authenticationHelper)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _authenticationHelper = authenticationHelper;
    }

    public async Task<AuthenticationResponse> Handle(LoginWithEmailAndPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing login for email: {Email}", request.UserCredentials.Email);

        var account = await _accountRepository.GetAccountByEmailAsync(request.UserCredentials.Email);
        if (account == null)
        {
            _logger.LogWarning("No account found for email: {Email}", request.UserCredentials.Email);
            return new AuthenticationResponse
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        if (!_authenticationHelper.VerifyPassword(request.UserCredentials.Password, account.HashedPassword))
        {
            _logger.LogWarning("Invalid password for email: {Email}", request.UserCredentials.Email);
            return new AuthenticationResponse
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var token = _authenticationHelper.GenerateJwtTokenString(account);
        _logger.LogInformation("Successfully logged in user with email: {Email}", request.UserCredentials.Email);

        return new AuthenticationResponse
        {
            Token = token,
            Success = true,
            Message = "Login successful",
            User = account
        };
    }
}
