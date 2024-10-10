using Microsoft.AspNetCore.Server.Kestrel;

namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class ServiceConfiguration
{
    public const string ConfigurationName = "ServiceConfiguration";

    public ServiceInfoConfiguration ServiceInfo { get; set; }

    public LoggingSinksConfiguration LoggingSinks { get; set; }

    public LoggingConfiguration Logging { get; set; }

    public ServiceEndpointConfiguration ServiceEndpoint { get; set; }

    public AuthenticationConfiguration Authentication { get; set; }
}
