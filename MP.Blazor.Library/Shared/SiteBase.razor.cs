using Microsoft.AspNetCore.Components;
using MP.Blazor.Library.Models;
using MP.Blazor.Library.Services.Interfaces;

namespace MP.Blazor.Library.Shared
{
    public partial class SiteBase : ComponentBase
    {
        [Parameter]
        public RenderFragment? Body { get; set; }

        [Inject]
        public ITranslationService? TranslationService { get; set; }

        private ITranslationService TS { get => TranslationService == null ? throw new NullReferenceException() : TranslationService; }

        [Inject]
        public SiteDescription? InjectedSiteDescription { get; set; }

        private SiteDescription SiteDescription { get => InjectedSiteDescription == null ? throw new NullReferenceException() : InjectedSiteDescription; }

        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async override Task OnInitializedAsync()
        {
            if (TS == null)
            {
                throw new NullReferenceException("Something has gone wrong with dependency injection");
            }

            await TS.Initialize();

            await base.OnInitializedAsync();

            await InvokeAsync(() => StateHasChanged());
        }
    }
}
