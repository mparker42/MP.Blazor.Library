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
        public static IServiceCollection AddBaseLibrary(this IServiceCollection services, SiteDescription siteDescription, IConfiguration configuration)
        {
            services.AddMudServices();
            services.AddHttpClient();
            services.AddSingleton(siteDescription);
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddBlazorApplicationInsights(async applicationInsights =>
            {
                await applicationInsights.SetInstrumentationKey(configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
                await applicationInsights.LoadAppInsights();
            });

            return services;
        }
    }
}
