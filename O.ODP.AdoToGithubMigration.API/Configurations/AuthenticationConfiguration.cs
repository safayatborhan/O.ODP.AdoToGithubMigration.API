namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class AuthenticationConfiguration
{
    public bool Enabled { get; set; }

    public AzureAdConfiguration AzureAd { get; set; }

    public ApiKeyConfiguration ApiKey { get; set; }
}
