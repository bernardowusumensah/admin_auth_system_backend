using System;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.DTOs;

public record AuthenticationResponse
{
    public string Token { get; init; } = string.Empty;
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;

    public Account User { get; init; } = new Account();
}
