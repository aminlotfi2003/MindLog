using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Profiles.Dtos;

public sealed record CreateProfileDto(
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
    public static CreateProfileDto FromEntity(PersonalProfile profile) =>
        new(
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
