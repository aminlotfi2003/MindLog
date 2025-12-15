using MediatR;
using MindLog.Application.Features.Identity.Dtos;

namespace MindLog.Application.Features.Identity.Commands.AdminLogin;

public sealed record AdminLoginCommand(
    string Email,
    string Password,
    bool RememberMe,
    string? IpAddress,
    string? Host
) : IRequest<AdminLoginResult>;
