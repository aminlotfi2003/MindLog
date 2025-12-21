using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.WorkExperienes.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetWorkExperieneDetails;

public record class GetWorkExperieneDetailsQueryHandler : IRequestHandler<GetWorkExperieneDetailsQuery, WorkExperieneDetailsDto>
{
    private readonly IWorkExperienceRepository _repo;

    public GetWorkExperieneDetailsQueryHandler(IWorkExperienceRepository repo)
    {
        _repo = repo;
    }

    public async Task<WorkExperieneDetailsDto> Handle(GetWorkExperieneDetailsQuery request, CancellationToken cancellationToken)
    {
        var experience = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (experience is null)
            throw new NotFoundException($"Work experience with ID '{request.Id}' was not found.");

        return WorkExperieneDetailsDto.FromEntity(experience);
    }
}
