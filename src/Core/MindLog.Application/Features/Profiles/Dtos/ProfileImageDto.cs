namespace MindLog.Application.Features.Profiles.Dtos;

public record ProfileImageDto(
    string FileName,
    string ContentType,     // image/png, image/jpeg, image/webp
    long SizeBytes,
    DateTimeOffset UploadedAt
);
