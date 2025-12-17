using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Infrastructure.Persistence.Configurations;
using MindLog.Infrastructure.Persistence.Contexts;
using MindLog.Infrastructure.Persistence.Repositories.Application;
using MindLog.Infrastructure.Persistence.Repositories.Identity;
using MindLog.Infrastructure.Persistence.UnitOfWork;

namespace MindLog.Infrastructure.Persistence.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Default")
                   ?? throw new InvalidOperationException("ConnectionStrings: Default is missing.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connection, sql =>
            {
                sql.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Application);
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // Register Application Repositories
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IEducationRecordRepository, EducationRecordRepository>();
        services.AddScoped<ILanguageProficiencyRepository, LanguageProficiencyRepository>();
        services.AddScoped<IPersonalProfileRepository, PersonalProfileRepository>();
        services.AddScoped<ISkillItemRepository, SkillItemRepository>();
        services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
        services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

        // Register Identity Repositories
        services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        services.AddScoped<IUserPasswordHistoryRepository, UserPasswordHistoryRepository>();
        services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
