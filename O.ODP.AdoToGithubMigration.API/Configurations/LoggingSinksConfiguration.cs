namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class LoggingSinksConfiguration
{
    public const string ConfigurationName = "LoggingSinks";

    public RollingFileSinkConfiguration RollingFile { get; set; }

    public ConsoleSinkConfiguration Console { get; set; }
}
