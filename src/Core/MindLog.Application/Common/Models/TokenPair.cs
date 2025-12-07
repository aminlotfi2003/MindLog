namespace MindLog.Application.Common.Models;

public sealed record TokenPair(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    string RefreshTokenHash
);
