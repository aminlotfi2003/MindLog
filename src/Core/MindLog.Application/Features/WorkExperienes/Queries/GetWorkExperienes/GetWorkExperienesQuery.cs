using MediatR;
using MindLog.Application.Features.WorkExperienes.Dtos;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetWorkExperienes;

public record GetWorkExperienesQuery : IRequest<IReadOnlyList<WorkExperieneListItemDto>>;
