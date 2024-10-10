namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IResourceSource
{
    public (string name, string url) GetResourceSource();
}
