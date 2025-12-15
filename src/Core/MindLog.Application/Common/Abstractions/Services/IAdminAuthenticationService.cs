using MindLog.Application.Features.Identity.Dtos;

namespace MindLog.Application.Common.Abstractions.Services;

public interface IAdminAuthenticationService
{
    Task<AdminLoginResult> SignInAsync(
        string email,
        string password,
        bool rememberMe,
        string? ipAddress,
        string? host,
        CancellationToken cancellationToken = default);
}
