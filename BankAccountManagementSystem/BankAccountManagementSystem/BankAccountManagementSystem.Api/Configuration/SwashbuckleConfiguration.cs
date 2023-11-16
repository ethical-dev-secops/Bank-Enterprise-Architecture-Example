using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BankAccountManagementSystem.Api.Filters;
using BankAccountManagementSystem.Application;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Swashbuckle.SwashbuckleConfiguration", Version = "1.0")]

namespace BankAccountManagementSystem.Api.Configuration
{
    public static class SwashbuckleConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionSwaggerGenOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
                    options.SupportNonNullableReferenceTypes();
                    options.CustomSchemaIds(x => x.FullName);

                    var apiXmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                    if (File.Exists(apiXmlFile))
                    {
                        options.IncludeXmlComments(apiXmlFile);
                    }

                    var applicationXmlFile = Path.Combine(AppContext.BaseDirectory, $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml");
                    if (File.Exists(applicationXmlFile))
                    {
                        options.IncludeXmlComments(applicationXmlFile);
                    }
                    options.OperationFilter<AuthorizeCheckOperationFilter>();
                    var authorizationUrl = configuration.GetValue<Uri>("Swashbuckle:Security:OAuth2:Implicit:AuthorizationUrl");
                    var tokenUrl = configuration.GetValue<Uri>("Swashbuckle:Security:OAuth2:Implicit:TokenUrl");
                    var clientId = configuration.GetValue<string>("Swashbuckle:Security:OAuth2:Implicit:ClientId");
                    var scopes = configuration.GetSection("Swashbuckle:Security:OAuth2:Implicit:Scope").Get<Dictionary<string, string>>()!.ToDictionary(x => x.Value, x => x.Key);

                    var securityScheme = new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                Scopes = scopes,
                                AuthorizationUrl = authorizationUrl,
                                TokenUrl = tokenUrl
                            }
                        }
                    };

                    options.AddSecurityDefinition("Implicit", securityScheme);
                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            { securityScheme, scopes.Select(s => s.Key).ToArray() }
                        });
                });
            return services;
        }

        public static void UseSwashbuckle(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "swagger";
                    options.OAuthAppName("BankAccountManagementSystem API");
                    options.EnableDeepLinking();
                    options.DisplayOperationId();
                    options.DefaultModelsExpandDepth(2);
                    options.DefaultModelRendering(ModelRendering.Model);
                    options.DocExpansion(DocExpansion.List);
                    options.ShowExtensions();
                    options.EnableFilter(string.Empty);
                    AddSwaggerEndpoints(app, options);
                    var clientId = configuration.GetValue<string>("Swashbuckle:Security:OAuth2:Implicit:ClientId");
                    var clientSecret = configuration.GetValue<string>("Swashbuckle:Security:OAuth2:Implicit:ClientSecret");
                    options.OAuthClientId(clientId);
                    if (!string.IsNullOrWhiteSpace(clientSecret))
                    {
                        options.OAuthClientSecret(clientSecret);
                    }
                    options.OAuthScopeSeparator(" ");
                });
        }

        private static void AddSwaggerEndpoints(IApplicationBuilder app, SwaggerUIOptions options)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(o => o.ApiVersion))
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{options.OAuthConfigObject.AppName} {description.GroupName}");
            }
        }
    }

    internal class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var additionalRequiredProps = model.Properties
                .Where(x => !x.Value.Nullable && !model.Required.Contains(x.Key))
                .Select(x => x.Key);

            foreach (var propKey in additionalRequiredProps)
            {
                model.Required.Add(propKey);
            }
        }
    }
}