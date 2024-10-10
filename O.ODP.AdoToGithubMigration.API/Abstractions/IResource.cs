using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Abstractions;

public interface IResource
{
    public List<ResourceResponse> GetResources(string source);
}
