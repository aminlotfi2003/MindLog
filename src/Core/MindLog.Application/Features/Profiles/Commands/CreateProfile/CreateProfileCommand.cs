using MediatR;
using MindLog.Application.Features.Profiles.Dtos;

namespace MindLog.Application.Features.Profiles.Commands.CreateProfile;

public record CreateProfileCommand(
    string FullName,
    string? Summary,
    DateOnly? BirthDate,
    string? Email,
    string? PhoneNumber,
    string? Website,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? Address
) : IRequest<Guid>
{
    public static CreateProfileCommand FromDto(CreateProfileDto dto) =>
        new(
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
