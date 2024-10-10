using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Services;

public class ResourcePipeline : IResource
{
    private readonly IResourceSourceFactory _resourceSourceFactory;

    public ResourcePipeline(IResourceSourceFactory resourceSourceFactory)
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
                Name = "Pipeline1",
                Source = resourceSource.name
            },
            new ResourceResponse
            {
                Id = "2",
                Name = "Pipeline2",
                Source = resourceSource.name
            }
        };
    }
}
