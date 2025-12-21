using MediatR;
using MindLog.Application.Features.WorkExperienes.Dtos;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetDeletedWorkExperieneDetails;

public record GetDeletedWorkExperieneDetailsQuery(Guid Id) : IRequest<WorkExperieneDetailsDto>;
