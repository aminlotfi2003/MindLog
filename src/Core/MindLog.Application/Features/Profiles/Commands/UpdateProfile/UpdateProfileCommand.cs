using MediatR;
using MindLog.Application.Features.Profiles.Dtos;

namespace MindLog.Application.Features.Profiles.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid Id,
    string FullName,
    string? Summary,
    DateOnly? BirthDate,
    string? Email,
    string? PhoneNumber,
    string? Website,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? Address
) : IRequest<Unit>
{
    public static UpdateProfileCommand FromEntity(UpdateProfileDto dto) =>
        new(
            dto.Id,
            dto.FullName,
            dto.Summary,
            dto.BirthDate,
            dto.Email,
            dto.PhoneNumber,
            dto.Website,
            dto.LinkedInUrl,
            dto.GitHubUrl,
            dto.Address
        );
}
