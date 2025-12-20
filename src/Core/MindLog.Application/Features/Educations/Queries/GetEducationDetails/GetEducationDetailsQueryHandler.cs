using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Queries.GetEducationDetails;

public class GetEducationDetailsQueryHandler : IRequestHandler<GetEducationDetailsQuery, EducationDetailsDto>
{
    private readonly IEducationRecordRepository _repo;

    public GetEducationDetailsQueryHandler(IEducationRecordRepository repo)
    {
        _repo = repo;
    }

    public async Task<EducationDetailsDto> Handle(GetEducationDetailsQuery request, CancellationToken cancellationToken)
    {
        var education = await _repo.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        if (education is null)
            throw new NotFoundException($"Education with ID '{request.Id}' was not found.");

        return EducationDetailsDto.FromEntity(education);
    }
}
