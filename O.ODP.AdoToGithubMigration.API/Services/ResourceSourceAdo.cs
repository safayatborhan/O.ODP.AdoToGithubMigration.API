using O.ODP.AdoToGithubMigration.API.Abstractions;

namespace O.ODP.AdoToGithubMigration.API.Services;

public class ResourceSourceAdo : IResourceSource
{
    public (string name, string url) GetResourceSource()
    {
        return ("ado", "http://ado/com/migrate");
    }
}
