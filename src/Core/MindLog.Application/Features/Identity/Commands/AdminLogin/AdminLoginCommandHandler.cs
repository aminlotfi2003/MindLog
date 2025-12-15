using MediatR;
using MindLog.Application.Common.Abstractions.Services;
using MindLog.Application.Features.Identity.Dtos;

namespace MindLog.Application.Features.Identity.Commands.AdminLogin;

public sealed class AdminLoginCommandHandler(IAdminAuthenticationService authenticationService)
    : IRequestHandler<AdminLoginCommand, AdminLoginResult>
{
    private readonly IAdminAuthenticationService _authenticationService = authenticationService;

    public async Task<AdminLoginResult> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.SignInAsync(
            request.Email,
            request.Password,
            request.RememberMe,
            request.IpAddress,
            request.Host,
            cancellationToken);
    }
}
