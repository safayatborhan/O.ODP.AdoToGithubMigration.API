using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Services;

namespace O.ODP.AdoToGithubMigration.API.Factories;

public class MigrationFactory : IMigrationFactory
{
    private readonly IResourceSourceFactory _resourceSourceFactory;

    public MigrationFactory(IResourceSourceFactory resourceSourceFactory)
    {
        _resourceSourceFactory = resourceSourceFactory;
    }

    public IMigration GetMigration(string resourceType)
    {
        switch (resourceType)
        {
            case ResourceTypeConstants.Artifact:
                return new MigrationArtifact(_resourceSourceFactory);
            case ResourceTypeConstants.Board:
                return new MigrationBoard(_resourceSourceFactory);
            case ResourceTypeConstants.Repository:
                return new MigrationRepository(_resourceSourceFactory);
            case ResourceTypeConstants.Pipeline:
                return new MigrationPipeline(_resourceSourceFactory);
            default:
                throw new NotImplementedException();
        }
    }
}