using O.ODP.AdoToGithubMigration.API;
using O.ODP.AdoToGithubMigration.API.Configurations;
using O.ODP.AdoToGithubMigration.API.Extensions;
using Serilog;
using System.Reflection;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Log.Information("Starting up");

            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .UseSerilog((hostBuilderContext, loggingConfiguration) =>
        {
            var serviceConfiguration = hostBuilderContext.Configuration.GetSection(ServiceConfiguration.ConfigurationName).Get<ServiceConfiguration>();
            loggingConfiguration.ConfigureLoggerConfiguration(serviceConfiguration, hostBuilderContext.HostingEnvironment.ContentRootPath);
        })
        .ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(Path.GetDirectoryName(typeof(Program).GetTypeInfo().Assembly.Location));
        })
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}