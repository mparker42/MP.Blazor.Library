using BlazorApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.Blazor.Library.Models;
using MP.Blazor.Library.Services;
using MP.Blazor.Library.Services.Interfaces;
using MudBlazor.Services;

namespace MP.Blazor.Library
{
    public static class Dependencies
    {
        public static IServiceCollection AddBaseLibrary(this IServiceCollection services, SiteDescription siteDescription)
        {
            services
                .AddMudServices()
                .AddHttpClient()
                .AddSingleton(siteDescription)
                .AddSingleton<ITranslationService, TranslationService>()
                .AddBlazorApplicationInsights(async applicationInsights =>
                    {
                        var telemetryItem = new TelemetryItem()
                        {
                            Tags = new Dictionary<string, object>()
                            {
                                { "ai.cloud.role", siteDescription.SiteName ?? "SPA" }
                            }
                        };

                        await applicationInsights.AddTelemetryInitializer(telemetryItem);
                    }
                );

            return services;
        }
    }
}
