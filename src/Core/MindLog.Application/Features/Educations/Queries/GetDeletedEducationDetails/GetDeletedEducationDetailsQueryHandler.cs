using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Queries.GetDeletedEducationDetails;

public class GetDeletedEducationDetailsQueryHandler : IRequestHandler<GetDeletedEducationDetailsQuery, EducationDetailsDto>
{
    private readonly IEducationRecordRepository _repo;

    public GetDeletedEducationDetailsQueryHandler(IEducationRecordRepository repo)
    {
        _repo = repo;
    }

    public async Task<EducationDetailsDto> Handle(GetDeletedEducationDetailsQuery request, CancellationToken cancellationToken)
    {
        var education = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (education is null)
            throw new NotFoundException($"Education with ID '{request.Id}' was not found.");

        return EducationDetailsDto.FromEntity(education);
    }
}
