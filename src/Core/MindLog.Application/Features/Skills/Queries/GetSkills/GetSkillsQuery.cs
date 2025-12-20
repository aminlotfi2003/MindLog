using MediatR;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetSkills;

public record GetSkillsQuery : IRequest<IReadOnlyList<SkillListItemDto>>;
