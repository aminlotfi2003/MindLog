using MediatR;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetSkillDetails;

public record GetSkillDetailsQuery(Guid Id) : IRequest<SkillDetailsDto>;
