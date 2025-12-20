using MediatR;
using MindLog.Application.Features.Languages.Dtos;

namespace MindLog.Application.Features.Languages.Queries.GetLanguageDetails;

public record GetLanguageDetailsQuery(Guid Id) : IRequest<LanguageDetailsDto>;
