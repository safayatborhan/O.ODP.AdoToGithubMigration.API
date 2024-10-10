using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IMigration
{
    public MigrationResponse ProcessMigration();
}
