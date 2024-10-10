using Microsoft.AspNetCore.Mvc;
using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Models.RequestDTO;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Controllers;

[Route("api/v1")]
public class MigrationController : ControllerBase
{
    private readonly IMigrationFactory _migrationFactory;

    public MigrationController(IMigrationFactory migrationFactory)
    {
        _migrationFactory = migrationFactory;
    }

    [HttpPost("orgs/{id}/migration/{type}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<ActionResult<MigrationResponse>> InitiateMigration([FromRoute] string id, [FromRoute] string type, [FromBody] MigrationRequest migrationRequest)
    {
        var migrationFactory = _migrationFactory.GetMigration(type);
        var result = migrationFactory.ProcessMigration();
        return Accepted(result);
    }

    [HttpGet("orgs/{id}/migration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MigrationResponse>>> GetAllMigrations([FromRoute] string id)
    {
        var result = new List<MigrationResponse>();
        return Ok(result);
    }

    [HttpGet("orgs/{orgId}/migration/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MigrationResponse>> GetMigrationDetails([FromRoute] string orgId, [FromRoute] string id)
    {
        var result = new MigrationResponse();
        return Ok(result);
    }

    [HttpDelete("orgs/{orgId}/migration/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MigrationResponse>> RevertMigration([FromRoute] string orgId, [FromRoute] string id)
    {
        var result = new MigrationResponse();
        return Accepted(result);
    }
}
