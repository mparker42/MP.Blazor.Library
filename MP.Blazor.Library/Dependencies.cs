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
            services.AddMudServices();
            services.AddHttpClient();
            services.AddSingleton(siteDescription);
            services.AddSingleton<ITranslationService, TranslationService>();

            return services;
        }
    }
}
