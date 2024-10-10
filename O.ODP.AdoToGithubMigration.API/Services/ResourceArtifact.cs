using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Services;

public class ResourceArtifact : IResource
{
    private readonly IResourceSourceFactory _resourceSourceFactory;

    public ResourceArtifact(IResourceSourceFactory resourceSourceFactory)
    {
        _resourceSourceFactory = resourceSourceFactory;
    }

    public List<ResourceResponse> GetResources(string source)
    {
        var resourceSource = _resourceSourceFactory.GetResourceSource(source).GetResourceSource();

        return new List<ResourceResponse>
        {
            new ResourceResponse
            {
                Id = "1",
                Name = "Artifact1",
                Source = resourceSource.name
            },
            new ResourceResponse
            {
                Id = "2",
                Name = "Artifact2",
                Source = resourceSource.name
            }
        };
    }
}
