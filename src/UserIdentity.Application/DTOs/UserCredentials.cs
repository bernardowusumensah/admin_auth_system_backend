using System;

namespace UserIdentity.Application.DTOs;

public record UserCredentials {
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
