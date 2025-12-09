using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetAuthorDetails;

public sealed record GetAuthorDetailsQuery(Guid Id) : IRequest<AuthorDetailsDto>;
