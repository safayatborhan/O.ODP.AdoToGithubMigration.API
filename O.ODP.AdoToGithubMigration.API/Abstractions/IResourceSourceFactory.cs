namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IResourceSourceFactory
{
    public IResourceSource GetResourceSource(string type);
}
