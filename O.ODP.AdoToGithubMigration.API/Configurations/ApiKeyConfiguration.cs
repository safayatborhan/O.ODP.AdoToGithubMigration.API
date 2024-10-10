namespace O.ODP.AdoToGithubMigration.API.Configurations;

public class ApiKeyConfiguration
{
    public bool Enabled { get; set; }

    //NOTE: do not use colon ":" in dictionary keys, it will break the structure!
    //https://stackoverflow.com/questions/52121374/net-core-configuration-manager-reading-a-dictionary-where-one-key-has-a-colon
    public Dictionary<string, string> ApiKeys { get; set; }
}
