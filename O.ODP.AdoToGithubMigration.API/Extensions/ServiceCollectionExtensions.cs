using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Factories;

namespace O.ODP.AdoToGithubMigration.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventDataService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IResourceSourceFactory, ResourceSourceFactory>();
        services.AddSingleton<IResourceFactory, ResourceFactory>();
        services.AddSingleton<IMigrationFactory, MigrationFactory>();

        return services;
    }
}
