namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class AzureAdConfiguration
{
    public bool Enabled { get; set; }

    public string Instance { get; set; }

    public string TenantId { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    //NOTE: do not use colon ":" in dictionary keys, it will break the structure!
    //https://stackoverflow.com/questions/52121374/net-core-configuration-manager-reading-a-dictionary-where-one-key-has-a-colon
    public List<string> Scopes { get; set; }

    //NOTE : This is for RBAC
    public List<string> AppRoles { get; set; }
}
