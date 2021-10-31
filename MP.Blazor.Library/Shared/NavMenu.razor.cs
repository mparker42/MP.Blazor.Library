using Microsoft.AspNetCore.Components;
using MP.Blazor.Library.Models;
using MP.Blazor.Library.Services.Interfaces;

namespace MP.Blazor.Library.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        public ITranslationService? TranslationService { get; set; }

        private ITranslationService TS { get => TranslationService == null ? throw new NullReferenceException() : TranslationService; }

        [Parameter]
        public IEnumerable<NavigatablePage>? Pages { get; set; }
    }
}
