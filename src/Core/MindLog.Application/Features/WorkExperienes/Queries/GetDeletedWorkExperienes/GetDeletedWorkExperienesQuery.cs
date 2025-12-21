using MediatR;
using MindLog.Application.Features.WorkExperienes.Dtos;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetDeletedWorkExperienes;

public record GetDeletedWorkExperienesQuery : IRequest<IReadOnlyList<WorkExperieneListItemDto>>;
