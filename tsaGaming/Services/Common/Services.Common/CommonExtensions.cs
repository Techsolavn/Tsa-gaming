using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ocelot.Middleware;
using Services.Common.ReversedProxy;

namespace Services.Common
{
    public static class CommonExtensions
    {
        public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
        {
            // Shared configuration via key vault
            //builder.Configuration.AddKeyVault();

            // Shared app insights configuration
            //builder.Services.AddApplicationInsights(builder.Configuration);

            // Default health checks assume the event bus and self health checks
            builder.Services.AddDefaultHealthChecks(builder.Configuration);

            // Add the event bus
            //builder.Services.AddEventBus(builder.Configuration);

            //builder.Services.AddDefaultAuthentication(builder.Configuration);

            builder.Services.AddDefaultOpenApi(builder.Configuration);

            // Add the accessor
            builder.Services.AddHttpContextAccessor();

            return builder;
        }
        public static IHealthChecksBuilder AddDefaultHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            // Health check for the application itself
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            
            return hcBuilder;

            //var eventBusSection = configuration.GetSection("EventBus");

            //if (!eventBusSection.Exists())
            //{
            //    return hcBuilder;
            //}

            //return eventBusSection["ProviderName"]?.ToLowerInvariant() switch
            //{
            //    "servicebus" => hcBuilder.AddAzureServiceBusTopic(
            //            _ => configuration.GetRequiredConnectionString("EventBus"),
            //            _ => "eshop_event_bus",
            //            name: "servicebus",
            //            tags: new string[] { "ready" }),

            //    _ => hcBuilder.AddRabbitMQ(
            //            _ => $"amqp://{configuration.GetRequiredConnectionString("EventBus")}",
            //            name: "rabbitmq",
            //            tags: new string[] { "ready" })
            //};
        }
        public static WebApplication UseServiceDefaults(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var pathBase = app.Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
                app.UseRouting();

                //var identitySection = app.Configuration.GetSection("Identity");

                //if (identitySection.Exists())
                //{
                //    // We have to add the auth middleware to the pipeline here
                //    app.UseAuthentication();
                //    app.UseAuthorization();
                //}
            }

            app.UseDefaultOpenApi(app.Configuration);

            app.MapDefaultHealthChecks();

            return app;
        }
        public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app, IConfiguration configuration)
        {
            var openApiSection = configuration.GetSection("OpenApi");

            if (!openApiSection.Exists())
            {
                return app;
            }

            app.UseSwagger();
            var routes = configuration.GetSection("Routes").Get<List<OcelotConfiguration>>();
            //var reversedProxy = configuration.GetSection("ReverseProxy").Get();
            var reversedProxy = app.Services.GetRequiredService<IOptionsMonitor<ReverseProxyDocumentFilterConfig>>().CurrentValue;
            if (routes == null && reversedProxy.Clusters == null)
            {
                app.UseSwaggerUI(setup =>
                {
                    /// {
                    ///   "OpenApi": {
                    ///     "Endpoint: {
                    ///         "Name": 
                    ///     },
                    ///     "Auth": {
                    ///         "ClientId": ..,
                    ///         "AppName": ..
                    ///     }
                    ///   }
                    /// }

                        var pathBase = configuration["PATH_BASE"];
                        //var authSection = openApiSection.GetSection("Auth");
                        var endpointSection = openApiSection.GetRequiredSection("Endpoint");

                        var swaggerUrl = endpointSection["Url"] ?? $"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json";

                        setup.SwaggerEndpoint(swaggerUrl, endpointSection.GetRequiredValue("Name"));

                    //if (authSection.Exists())
                    //{
                    //    setup.OAuthClientId(authSection.GetRequiredValue("ClientId"));
                    //    setup.OAuthAppName(authSection.GetRequiredValue("AppName"));
                    //}
                });

            }
            else if(reversedProxy.Clusters != null)
            {
                app.UseSwaggerUI(options =>
                {
                    var config = app.Services.GetRequiredService<IOptionsMonitor<ReverseProxyDocumentFilterConfig>>().CurrentValue;
                    foreach (var cluster in config.Clusters)
                    {
                        options.SwaggerEndpoint($"/swagger/{cluster.Key}/swagger.json", cluster.Key);
                    }
                });
            }
            // Add a redirect from the root of the app to the swagger endpoint
            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

            return app;
        }
        public static void MapDefaultHealthChecks(this IEndpointRouteBuilder routes)
        {
            routes.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            routes.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        }
        public static IServiceCollection AddDefaultOpenApi(this IServiceCollection services, IConfiguration configuration)
        {
            var openApi = configuration.GetSection("OpenApi");

            if (!openApi.Exists())
            {
                return services;
            }

            services.AddEndpointsApiExplorer();
            var routes = configuration.GetSection("Routes").Get<List<OcelotConfiguration>>();
            return services.AddSwaggerGen(options =>
            {
                /// {
                ///   "OpenApi": {
                ///     "Document": {
                ///         "Title": ..
                ///         "Version": ..
                ///         "Description": ..
                ///     }
                ///   }
                /// }
                var document = openApi.GetRequiredSection("Document");

                var version = document.GetRequiredValue("Version") ?? "v1";

                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = document.GetRequiredValue("Title"),
                    Version = version,
                    Description = document.GetRequiredValue("Description")
                });

                //    var identitySection = configuration.GetSection("Identity");

                //    if (!identitySection.Exists())
                //    {
                //        // No identity section, so no authentication open api definition
                //        return;
                //    }

                //    // {
                //    //   "Identity": {
                //    //     "ExternalUrl": "http://identity",
                //    //     "Scopes": {
                //    //         "basket": "Basket API"
                //    //      }
                //    //    }
                //    // }

                //    var identityUrlExternal = identitySection["ExternalUrl"] ?? identitySection.GetRequiredValue("Url");
                //    var scopes = identitySection.GetRequiredSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value);

                //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //    {
                //        Type = SecuritySchemeType.OAuth2,
                //        Flows = new OpenApiOAuthFlows()
                //        {
                //            Implicit = new OpenApiOAuthFlow()
                //            {
                //                AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                //                TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                //                Scopes = scopes,
                //            }
                //        }
                //    });

                //    options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
    
}
