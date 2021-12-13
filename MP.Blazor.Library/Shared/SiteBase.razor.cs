using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MP.Blazor.Library.Models;
using MP.Blazor.Library.Services.Interfaces;
using MudBlazor;

namespace MP.Blazor.Library.Shared
{
    public partial class SiteBase : ComponentBase
    {
        [Parameter]
        public RenderFragment? Body { get; set; }

        [Inject]
        public ITranslationService? TranslationService { get; set; }

        [Inject]
        public IJSRuntime? JsRuntime { get; set; }

        private ITranslationService TS { get => TranslationService == null ? throw new NullReferenceException() : TranslationService; }

        [Inject]
        public SiteDescription? InjectedSiteDescription { get; set; }

        private bool _prefersDarkTheme = false;

        public MudTheme Theme { get; set; } = new MudTheme();

        private SiteDescription SiteDescription { get => InjectedSiteDescription == null ? throw new NullReferenceException() : InjectedSiteDescription; }

        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async override Task OnInitializedAsync()
        {
            if (TS == null || JsRuntime == null)
            {
                throw new NullReferenceException("Something has gone wrong with dependency injection");
            }

            var darkThemeModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "_content/MP.Blazor.Library/library-js/darkmode.js");

            Console.WriteLine(darkThemeModule);

            _prefersDarkTheme = await darkThemeModule.InvokeAsync<bool>("prefersDarkMode");

            SiteDescription.Themes ??= new Dictionary<ThemeType, SiteTheme>();

            MudTheme?
                darkTheme = null,
                lightTheme = null;

            if (SiteDescription.Themes.ContainsKey(ThemeType.Dark))
            {
                darkTheme = SiteDescription.Themes[ThemeType.Dark];
            }

            if (SiteDescription.Themes.ContainsKey(ThemeType.Light))
            {
                lightTheme = SiteDescription.Themes[ThemeType.Light];
            }
            else
            {
                lightTheme = darkTheme ?? new MudTheme();
            }

            darkTheme ??= lightTheme;

            Theme = _prefersDarkTheme ? darkTheme : lightTheme;

            await TS.Initialize();

            await base.OnInitializedAsync();

            await InvokeAsync(() => StateHasChanged());
        }
    }
}
