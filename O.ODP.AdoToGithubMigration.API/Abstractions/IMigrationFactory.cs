namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IMigrationFactory
{
    IMigration GetMigration(string resourceType);
}