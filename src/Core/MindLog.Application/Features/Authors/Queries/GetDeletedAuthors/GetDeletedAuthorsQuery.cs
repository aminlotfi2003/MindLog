using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetDeletedAuthors;

public sealed record GetDeletedAuthorsQuery : IRequest<IReadOnlyList<AuthorListItemDto>>;
