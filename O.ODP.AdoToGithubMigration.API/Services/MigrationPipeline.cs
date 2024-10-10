﻿using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Services;

public class MigrationPipeline : IMigration
{
    private readonly IResourceSourceFactory _resourceSourceFactory;

    public MigrationPipeline(IResourceSourceFactory resourceSourceFactory)
    {
        _resourceSourceFactory = resourceSourceFactory;
    }

    public MigrationResponse ProcessMigration()
    {
        var adoSource = _resourceSourceFactory.GetResourceSource("ado").GetResourceSource();
        var githubSource = _resourceSourceFactory.GetResourceSource("github").GetResourceSource();

        // process migration from ado to github

        return new MigrationResponse
        {
            MigrationId = "1",
            MigrationType = ResourceTypeConstants.Pipeline,
            Status = MigrationStatusConstants.InProgress,
        };
    }
}
