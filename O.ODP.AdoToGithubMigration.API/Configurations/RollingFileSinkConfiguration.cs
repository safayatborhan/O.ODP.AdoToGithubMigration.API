using Serilog;

namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class RollingFileSinkConfiguration
{
    public bool Enabled { get; set; }

    public string FilePath { get; set; }

    public string OutputTemplate { get; set; }

    public int FileSizeLimitInMB { get; set; }

    public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

    public int RetainedFileTimeLimitInDays { get; set; }
}
