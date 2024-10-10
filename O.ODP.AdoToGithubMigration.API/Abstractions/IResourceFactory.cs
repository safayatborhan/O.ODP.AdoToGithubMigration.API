namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IResourceFactory
{
    IResource GetResource(string resourceType);
}
