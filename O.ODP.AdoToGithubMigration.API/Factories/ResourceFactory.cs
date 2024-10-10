using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Services;

namespace O.ODP.AdoToGithubMigration.API.Factories;

public class ResourceFactory : IResourceFactory
{
    private readonly IResourceSourceFactory _resourceSourceFactory;

    public ResourceFactory(IResourceSourceFactory resourceSourceFactory)
    {
        _resourceSourceFactory = resourceSourceFactory;
    }

    public IResource GetResource(string resourceType)
    {
        switch (resourceType)
        {
            case ResourceTypeConstants.Artifact:
                return new ResourceArtifact(_resourceSourceFactory);
            case ResourceTypeConstants.Board:
                return new ResourceBoard(_resourceSourceFactory);
            case ResourceTypeConstants.Repository:
                return new ResourceRepository(_resourceSourceFactory);
            case ResourceTypeConstants.Pipeline:
                return new ResourcePipeline(_resourceSourceFactory);
            default:
                throw new NotImplementedException();
        }
    }
}
