using MediatR;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetDeletedSkillDetails;

public record GetDeletedSkillDetailsQuery(Guid Id) : IRequest<SkillDetailsDto>;
