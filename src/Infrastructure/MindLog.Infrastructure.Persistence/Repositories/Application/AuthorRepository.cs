using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class AuthorRepository : EfRepository<Guid, Author>, IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Author> _authors;

    public AuthorRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _authors = _dbContext.Set<Author>();
    }

    public async Task<Author?> GetWithBooksAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }
}
