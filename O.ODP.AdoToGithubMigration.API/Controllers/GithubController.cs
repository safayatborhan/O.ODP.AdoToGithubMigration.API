using Microsoft.AspNetCore.Mvc;
using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Controllers;

[Route("api/v1")]
public class GithubController : ControllerBase
{
    private readonly IResourceFactory _resourceFactory;

    public GithubController(IResourceFactory resourceFactory)
    {
        _resourceFactory = resourceFactory;
    }

    [HttpGet("orgs/{id}/github/{type}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ResourceResponse>>> GetResources([FromRoute] string id, [FromRoute] string type)
    {
        var resourceFactory = _resourceFactory.GetResource(type);
        var result = resourceFactory.GetResources(ResourceSourceConstants.Github);
        return Ok(result);
    }
}
