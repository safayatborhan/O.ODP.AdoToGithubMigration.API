using Serilog.Events;

namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class LoggingConfiguration
{
    public const string ConfigurationName = "Logging";

    public LogEventLevel Default { get; set; }

    public Dictionary<string, LogEventLevel> Override { get; set; } = new Dictionary<string, LogEventLevel>();
}
