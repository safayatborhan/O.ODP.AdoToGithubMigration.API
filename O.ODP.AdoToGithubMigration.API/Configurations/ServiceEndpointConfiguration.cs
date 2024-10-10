namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class ServiceEndpointConfiguration
{
    public const string ConfigurationName = "ServiceEndpoint";

    public EndPointConfiguration Http { get; set; }

    public EndPointConfiguration Https { get; set; }

    public CorsConfiguration Cors { get; set; }
}
