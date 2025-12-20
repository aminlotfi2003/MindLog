using MediatR;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetDeletedEducationDetails;

public record GetDeletedEducationDetailsQuery(Guid Id) : IRequest<EducationDetailsDto>;
