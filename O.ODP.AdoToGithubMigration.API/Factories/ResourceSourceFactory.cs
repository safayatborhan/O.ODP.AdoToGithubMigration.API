using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Services;

namespace O.ODP.AdoToGithubMigration.API.Factories;

public class ResourceSourceFactory : IResourceSourceFactory
{
    public IResourceSource GetResourceSource(string type)
    {
        switch (type)
        {
            case ResourceSourceConstants.Github:
                return new ResourceSourceGithub();
            case ResourceSourceConstants.Ado:
                return new ResourceSourceAdo();
            default:
                throw new NotImplementedException();
        }
    }
}
