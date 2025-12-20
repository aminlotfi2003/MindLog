using MediatR;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetEducationDetails;

public record GetEducationDetailsQuery(Guid Id) : IRequest<EducationDetailsDto>;
