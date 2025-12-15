namespace MindLog.Application.Features.Identity.Dtos;

public sealed record AdminLoginResult
{
    public bool Succeeded { get; init; }
    public bool IsLockedOut { get; init; }
    public string? ErrorMessage { get; init; }

    public static AdminLoginResult Success() => new() { Succeeded = true };

    public static AdminLoginResult LockedOut(string message) => new()
    {
        IsLockedOut = true,
        ErrorMessage = message
    };

    public static AdminLoginResult Failed(string message) => new() { ErrorMessage = message };
}
