namespace MindLog.Domain.ValueObjects;

public sealed record ProfileImage(
    string FileName,
    string ContentType,     // image/png, image/jpeg, image/webp
    long SizeBytes,
    DateTimeOffset UploadedAt
);
