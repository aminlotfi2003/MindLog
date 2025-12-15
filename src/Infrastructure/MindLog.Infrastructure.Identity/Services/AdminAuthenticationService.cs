using Microsoft.AspNetCore.Identity;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Common.Abstractions.Services;
using MindLog.Application.Features.Identity.Dtos;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Identity.Services;

public sealed class AdminAuthenticationService : IAdminAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserLoginHistoryRepository _loginHistoryRepository;

    public AdminAuthenticationService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IUserLoginHistoryRepository loginHistoryRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _loginHistoryRepository = loginHistoryRepository;
    }

    public async Task<AdminLoginResult> SignInAsync(
        string email,
        string password,
        bool rememberMe,
        string? ipAddress,
        string? host,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return AdminLoginResult.Failed("اطلاعات ورود اشتباه است.");

        if (!await _userManager.IsInRoleAsync(user, "Admin"))
            return AdminLoginResult.Failed("تنها مدیر می‌تواند وارد شود.");

        var failuresBeforeAttempt = await _userManager.GetAccessFailedCountAsync(user);

        var signInResult = await _signInManager.PasswordSignInAsync(
            user,
            password,
            rememberMe,
            lockoutOnFailure: true);

        var failureCount = signInResult.Succeeded
            ? failuresBeforeAttempt
            : await _userManager.GetAccessFailedCountAsync(user);

        await SaveLoginAttemptAsync(
            user.Id,
            signInResult.Succeeded,
            failureCount,
            ipAddress,
            host,
            cancellationToken);

        if (signInResult.Succeeded)
            return AdminLoginResult.Success();

        if (signInResult.IsLockedOut)
            return AdminLoginResult.LockedOut("حساب شما به دلیل تلاش‌های ناموفق قفل شده است. بعداً دوباره تلاش کنید.");

        return AdminLoginResult.Failed("اطلاعات ورود اشتباه است.");
    }

    private async Task SaveLoginAttemptAsync(
        Guid userId,
        bool success,
        int failureCount,
        string? ipAddress,
        string? host,
        CancellationToken cancellationToken)
    {
        var history = new UserLoginHistory
        {
            UserId = userId,
            Success = success,
            FailureCountBeforeSuccess = failureCount,
            IpAddress = ipAddress,
            Host = host,
            OccurredAt = DateTimeOffset.UtcNow
        };

        await _loginHistoryRepository.AddAsync(history, cancellationToken);
        await _loginHistoryRepository.SaveChangesAsync(cancellationToken);
    }
}
