using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Profiles.Dtos;

namespace MindLog.Application.Features.Profiles.Queries.GetProfiles;

public class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, IReadOnlyList<ProfileListItemDto>>
{
    private readonly IPersonalProfileRepository _repo;

    public GetProfilesQueryHandler(IPersonalProfileRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<ProfileListItemDto>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
    {
        var profiles = await _repo.ListAsync(cancellationToken);

        return profiles.Select(ProfileListItemDto.FromEntity).ToList();
    }
}
