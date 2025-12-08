using MindLog.Infrastructure.Persistence.Extensions.DependencyInjection;

namespace MindLog.WebApp.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructurePersistence(configuration);

        return services;
    }
}
