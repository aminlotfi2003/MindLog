using Microsoft.AspNetCore.Identity;
using MindLog.Application.Extensions.DependencyInjection;
using MindLog.Domain.Identity;
using MindLog.Infrastructure.Identity.Extensions.DependencyInjection;
using MindLog.Infrastructure.Persistence.Contexts;
using MindLog.Infrastructure.Persistence.Extensions.DependencyInjection;

namespace MindLog.WebApp.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructureIdentity(configuration);
        services.AddInfrastructurePersistence(configuration);

        return services;
    }

    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await ApplicationDbContextSeed.SeedAdminUserAsync(userManager, roleManager);
    }
}
