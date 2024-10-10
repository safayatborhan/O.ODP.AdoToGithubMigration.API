using O.ODP.AdoToGithubMigration.API.Configurations;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace O.ODP.AdoToGithubMigration.API.Extensions;

public static class LoggerConfigurationExtensions
{
    /// <summary>
    /// Configures the logger configuration.
    /// </summary>
    /// <param name="loggerConfiguration">The logger configuration.</param>
    /// <param name="serviceConfiguration">The service configuration.</param>
    public static void ConfigureLoggerConfiguration(this LoggerConfiguration loggerConfiguration, ServiceConfiguration serviceConfiguration, string rootPath)
    {
        var serviceInfo = serviceConfiguration.ServiceInfo;
        var loggingSinks = serviceConfiguration.LoggingSinks;

        ConfigureLoggerConfiguration(loggerConfiguration, serviceInfo, loggingSinks, serviceConfiguration.Logging, rootPath);
    }

    private static void ConfigureLoggerConfiguration(
            LoggerConfiguration loggerConfiguration,
            ServiceInfoConfiguration serviceInfo,
            LoggingSinksConfiguration loggingSinks,
            LoggingConfiguration logging,
            string rootPath)
    {
        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = logging?.Default ?? LogEventLevel.Warning
        };
        if (logging != null)
        {
            foreach (var overrideItem in logging.Override)
            {
                loggerConfiguration.MinimumLevel.Override(overrideItem.Key, overrideItem.Value);
            }
        }

        loggerConfiguration
            .MinimumLevel.ControlledBy(levelSwitch)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("AppService", serviceInfo.Name)
            .Enrich.WithProperty("AppEnvironment", Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"))
            .Enrich.WithProperty("AppUserName", $"{Environment.UserDomainName}\\{Environment.UserName.ToUpper()}")
            .Enrich.WithProperty("AppServer", Environment.MachineName.ToUpper())
            .Destructure.ToMaximumDepth(10);

        loggerConfiguration.WriteTo.Console(outputTemplate: loggingSinks.Console.OutputTemplate, theme: SystemConsoleTheme.Literate);
    }
}