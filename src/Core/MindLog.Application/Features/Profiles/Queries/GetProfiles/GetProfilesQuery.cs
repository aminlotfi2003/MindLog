using MediatR;
using MindLog.Application.Features.Profiles.Dtos;

namespace MindLog.Application.Features.Profiles.Queries.GetProfiles;

public record GetProfilesQuery : IRequest<IReadOnlyList<ProfileListItemDto>>;
