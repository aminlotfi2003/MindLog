using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Profiles.Dtos;

public record ProfileDetailsDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ModifiedAt,
    string FullName,
    string? Summary,
    DateOnly? BirthDate,
    string? Email,
    string? PhoneNumber,
    string? Website,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? Address)
{
    public static ProfileDetailsDto FromEntity(PersonalProfile profile) =>
        new(
            profile.Id,
            profile.CreatedAt,
            profile.ModifiedAt,
            profile.FullName,
            profile.Summary,
            profile.BirthDate,
            profile.Email,
            profile.PhoneNumber,
            profile.Website,
            profile.LinkedInUrl,
            profile.GitHubUrl,
            profile.Address
        );
}
