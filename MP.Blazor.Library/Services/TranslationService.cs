using MP.Blazor.Library.Models;
using MP.Blazor.Library.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace MP.Blazor.Library.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly SiteDescription _siteDescription;
        private string? _locale;
        private IDictionary<string, string> _values;

        public TranslationService(IHttpClientFactory clientFactory, SiteDescription siteDescription)
        {
            _clientFactory = clientFactory;
            _siteDescription = siteDescription;
            _values = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Values { get => _values; }

        private string ResolveLocale()
        {
#pragma warning disable CS8604 // Possible null reference argument. I want a null reference exception if the value is null at this point.
            if (_siteDescription.SupportedLocales.Count() == 1)
#pragma warning restore CS8604 // Possible null reference argument.
            {
                _locale = _siteDescription.SupportedLocales.ElementAt(0);
            }
            else
            {
                // Some complex locale selection logic can go here that looks at the browser
                // locale to find the most suitable option from the supported languages.
                throw new NotImplementedException();
            }

            return _locale;
        }


        public async Task Initialize()
        {
            ResolveLocale();

#pragma warning disable CS8602 // Dereference of a possibly null reference. I want a null reference exception if the value is null at this point.
            var request = new HttpRequestMessage(HttpMethod.Get, _siteDescription.TranslationsLocation.AbsoluteUri.Replace("__locale__", _locale));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

#pragma warning disable CS8601 // Possible null reference assignment. I want a null reference exception if the value is null at this point.
                _values = JsonSerializer.Deserialize<IDictionary<string, string>>(stringResponse);
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        public string SafeGet(string key)
        {
            return _values.ContainsKey(key) ? _values[key] : "";
        }
    }
}
