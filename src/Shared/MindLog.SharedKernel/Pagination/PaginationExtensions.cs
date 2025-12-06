using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MindLog.SharedKernel.Pagination;

public static class PaginationExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        this IQueryable<T> query,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default)
    {
        paginationParams.Normalize();

        query = EnsureOrdered(query);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync(cancellationToken);

        return PaginatedResult<T>.Create(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
    }

    private static IQueryable<T> EnsureOrdered<T>(IQueryable<T> query)
    {
        if (query is IOrderedQueryable<T>)
            return query;

        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty is not null)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, idProperty);
            var converted = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);
            return query.OrderBy(lambda);
        }

        return query.OrderBy(_ => 0);
    }
}
