using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetAuthors;

public sealed record GetAuthorsQuery : IRequest<IReadOnlyList<AuthorListItemDto>>;
