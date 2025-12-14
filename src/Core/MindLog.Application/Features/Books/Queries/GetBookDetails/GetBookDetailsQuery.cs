using MediatR;
using MindLog.Application.Features.Books.Dtos;

namespace MindLog.Application.Features.Books.Queries.GetBookDetails;

public sealed record GetBookDetailsQuery(Guid Id, bool IncludeDeleted = false) : IRequest<BookDetailsDto>;
