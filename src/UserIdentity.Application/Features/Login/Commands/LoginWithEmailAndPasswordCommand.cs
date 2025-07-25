using System;
using MediatR;
using UserIdentity.Application.DTOs;

namespace UserIdentity.Application.Features.Login.Commands;

public record LoginWithEmailAndPasswordCommand(UserCredentials UserCredentials) : IRequest<AuthenticationResponse>;
