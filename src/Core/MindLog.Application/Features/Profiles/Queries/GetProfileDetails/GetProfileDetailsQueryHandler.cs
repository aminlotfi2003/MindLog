using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Profiles.Queries.GetProfileDetails;

public class GetProfileDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, ProfileDetailsDto>
{
    private readonly IPersonalProfileRepository _repo;

    public GetProfileDetailsQueryHandler(IPersonalProfileRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProfileDetailsDto> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var profile = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (profile is null)
            throw new NotFoundException($"Profile with ID '{request.Id}' was not found.");

        return ProfileDetailsDto.FromEntity(profile);
    }
}
