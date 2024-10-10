using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using O.ODP.AdoToGithubMigration.API.Configurations;
using O.ODP.AdoToGithubMigration.API.Extensions;
using System.Security.Claims;

namespace O.ODP.AdoToGithubMigration.API;

public class Startup
{
    private const string DefaultCorsPolicyName = "defaultCorsPolicy";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ServiceConfiguration>(Configuration.GetSection(ServiceConfiguration.ConfigurationName));

        //deserialize appsettings into service configuration instance
        var serviceConfiguration = Configuration.GetSection(ServiceConfiguration.ConfigurationName).Get<ServiceConfiguration>();

        services.AddEventDataService(Configuration);

        services
            .AddMicrosoftIdentityWebApiAuthentication(Configuration, "ServiceConfiguration:Authentication:AzureAd");

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Readers",
                policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimTypes.Role, serviceConfiguration.Authentication.AzureAd.AppRoles);
                });
        });

        services.AddControllers(config =>
        {
            config.Filters.Add(new AuthorizeFilter("Readers"));
        });

        // Add cors

        var origins = serviceConfiguration.ServiceEndpoint?.Cors?.AllowedOrigins?.ToArray() ?? Array.Empty<string>();
        services
            .AddCors(options =>
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                    builder
                    .WithOrigins(origins)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                )
            );

        services.AddSwaggerGenEx(serviceConfiguration);

        services.AddHttpClient();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        //deserialize appsettings into service configuration instance
        var serviceConfiguration = Configuration.GetSection(ServiceConfiguration.ConfigurationName).Get<ServiceConfiguration>();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .MapControllers()
            .RequireAuthorization();
        });

        app.UseSwaggerEx(serviceConfiguration, env);
    }
}
