namespace MP.Blazor.Library.Models
{
    public class SiteDescription
    {
        public IEnumerable<string>? SupportedLocales { get; set; }
        public Uri? TranslationsLocation { get; set; }
        public string? TitleTranslationKey { get; set; }
        public IEnumerable<NavigatablePage>? Pages { get; set; }
    }
}
