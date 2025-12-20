using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Profiles.Dtos;

public record ProfileListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    string FullName,
    DateOnly? BirthDate,
    string? Email,
    string? PhoneNumber)
{
    public static ProfileListItemDto FromEntity(PersonalProfile profile) =>
        new(
            profile.Id,
            profile.CreatedAt,
            profile.FullName,
            profile.BirthDate,
            profile.Email,
            profile.PhoneNumber
        );
}
