using O.ODP.AdoToGithubMigration.API.Abstractions;

namespace O.ODP.AdoToGithubMigration.API.Services;

public class ResourceSourceGithub : IResourceSource
{
    public (string name, string url) GetResourceSource()
    {
        return ("github", "http://github/com/migrate");
    }
}
