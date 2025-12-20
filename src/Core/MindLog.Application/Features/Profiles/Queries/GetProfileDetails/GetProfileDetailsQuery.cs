using MediatR;
using MindLog.Application.Features.Profiles.Dtos;

namespace MindLog.Application.Features.Profiles.Queries.GetProfileDetails;

public record GetProfileDetailsQuery(Guid Id) : IRequest<ProfileDetailsDto>;
