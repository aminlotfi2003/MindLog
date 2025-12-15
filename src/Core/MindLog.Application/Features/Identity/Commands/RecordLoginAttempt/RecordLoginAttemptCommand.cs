using MediatR;

namespace MindLog.Application.Features.Identity.Commands.RecordLoginAttempt;

public sealed record RecordLoginAttemptCommand(
    Guid UserId,
    bool Success,
    int FailureCountBeforeSuccess,
    string? IpAddress,
    string? Host
) : IRequest;
