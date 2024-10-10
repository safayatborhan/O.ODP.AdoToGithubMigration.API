namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class ServiceInfoConfiguration
{
    public const string ConfigurationName = "ServiceInfo";

    public string Name { get; set; }

    public string BasePath { get; set; }

    public string Description { get; set; }

    public string Maintainer { get; set; }
}
