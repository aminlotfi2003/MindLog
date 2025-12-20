using MediatR;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetDeletedEducations;

public record GetDeletedEducationsQuery : IRequest<IReadOnlyList<EducationListItemDto>>;
