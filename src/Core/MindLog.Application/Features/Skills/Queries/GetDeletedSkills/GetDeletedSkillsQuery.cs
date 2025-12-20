using MediatR;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetDeletedSkills;

public record GetDeletedSkillsQuery : IRequest<IReadOnlyList<SkillListItemDto>>;
