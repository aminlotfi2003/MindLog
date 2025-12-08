using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions;
using MindLog.Domain.Entities;
using MindLog.Domain.Identity;
using MindLog.SharedKernel.Abstractions;
using System.Linq.Expressions;

namespace MindLog.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();

    public DbSet<UserLoginHistory> UserLoginHistories => Set<UserLoginHistory>();
    public DbSet<UserPasswordHistory> UserPasswordHistories => Set<UserPasswordHistory>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Register Fluent API Configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Register Soft Delete Query Filters
        ApplySoftDeleteQueryFilters(builder);
    }

    private static void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
    {
        var softDeletableInterface = typeof(ISoftDeletable);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (!softDeletableInterface.IsAssignableFrom(clrType))
                continue;

            if (entityType.GetQueryFilter() != null)
                continue;

            var parameter = Expression.Parameter(clrType, "e");
            var isDeletedPropertyInfo = clrType.GetProperty(nameof(ISoftDeletable.IsDeleted));
            if (isDeletedPropertyInfo == null)
                continue;

            var isDeletedProperty = Expression.Property(parameter, isDeletedPropertyInfo);
            var compareExpression = Expression.Equal(
                isDeletedProperty,
                Expression.Constant(false, typeof(bool))
            );

            var lambda = Expression.Lambda(compareExpression, parameter);
            modelBuilder.Entity(clrType).HasQueryFilter(lambda);
        }
    }
}
