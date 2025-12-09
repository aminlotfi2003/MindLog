using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetDeletedAuthorDetails;

public sealed record GetDeletedAuthorDetailsQuery(Guid Id) : IRequest<AuthorDetailsDto>;
