namespace MindLog.Application.Features.Identity.Dtos;

public sealed record LoginHistoryDto(
    DateTimeOffset OccurredAt,
    string? IpAddress,
    string? Host,
    bool Success,
    int FailureCountBeforeSuccess
);
