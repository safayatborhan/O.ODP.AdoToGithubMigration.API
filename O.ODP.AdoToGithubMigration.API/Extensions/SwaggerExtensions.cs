using Microsoft.OpenApi.Models;
using O.ODP.AdoToGithubMigration.API.Configurations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace O.ODP.AdoToGithubMigration.API.Extensions;

public static class SwaggerExtensions
{
    /// <summary>
    /// Adds documentation to the api from the XML documentation/comments added in the controller class.
    /// </summary>
    /// <param name="options">The swagger options object.</param>
    public static void ConfigureDocumentationFromXmlComments(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath, true);

        xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
        xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        options.CustomOperationIds(apiDescription =>
        {
            return apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });
    }

    public static void AddSwaggerGenEx(this IServiceCollection sc, ServiceConfiguration serviceConfiguration)
    {
        sc.AddSwaggerGen(c =>
        {
            var azureAdConfig = serviceConfiguration.Authentication.AzureAd;

            c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header
                        },
                        new List <string> ()
                    }
                });

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{azureAdConfig.Instance}/{azureAdConfig.TenantId}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"{azureAdConfig.Instance}/{azureAdConfig.TenantId}/oauth2/v2.0/token"),
                        Scopes = azureAdConfig.Scopes.ToDictionary(scope => scope, x => string.Empty)
                    }
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ODP ADO to Github Migration", Version = "v1" });

            c.ConfigureDocumentationFromXmlComments();

        }).AddSwaggerGenNewtonsoftSupport();
    }

    public static void UseSwaggerEx(this IApplicationBuilder app, ServiceConfiguration serviceConfiguration, IWebHostEnvironment env)
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((doc, req) =>
            {
                var url = $"https://{req.Host.Value}{serviceConfiguration.ServiceInfo.BasePath}";

                doc.Servers = new OpenApiServer[] {
                        // This is due to aks ingress mapping in preprod environment. for preprod we supply FQDN for basePath (Its a hack for time being)
                        new OpenApiServer {
                            Url = url
                        }
                    };
            });
        });

        app.UseSwaggerUI(c =>
        {
            var prefix = env.EnvironmentName == "LocalDevelopment" ? "" : serviceConfiguration.ServiceInfo.BasePath;
            c.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "O.ODP.AdoToGithubMigration v1");
            c.OAuthClientId(serviceConfiguration.Authentication.AzureAd.ClientId);

            c.DisplayOperationId();
        });
    }
}
