namespace MP.Blazor.Library.Services.Interfaces
{
    public interface ITranslationService
    {
        public IDictionary<string, string> Values { get; }

        public string SafeGet(string key);

        public Task Initialize();
    }
}
