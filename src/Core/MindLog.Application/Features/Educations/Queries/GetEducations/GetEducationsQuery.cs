using MediatR;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetEducations;

public record GetEducationsQuery : IRequest<IReadOnlyList<EducationListItemDto>>;
