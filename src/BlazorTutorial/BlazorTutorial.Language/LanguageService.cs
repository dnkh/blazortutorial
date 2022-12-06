namespace BlazorTutorial.Language
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System.Globalization;

    public class LanguageService : ILanguageService
    {
        private readonly LanguageOptions languageOptions;
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime jSRuntime;

        public LanguageService(LanguageOptions languageOptions, NavigationManager navigationManager, IJSRuntime jSRuntime)
        {
            this.languageOptions = languageOptions;
            this.navigationManager = navigationManager;
            this.jSRuntime = jSRuntime;
        }

        public bool Exist(string lang)
        {
            return !string.IsNullOrEmpty(lang) && this.languageOptions.GetLanguages().Any(x => x.Equals(lang, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsLanguageRedirect(string uri)
        {
            return uri.Contains("langChanged");
        }

        public string GetUriLanguage()
        {
            var uri = this.navigationManager.Uri;
            if (string.IsNullOrEmpty(uri))
            {
                return string.Empty;
            }


            var uriBuilder = new UriBuilder(uri);
            var split = uriBuilder.Path.Split("/");

            if (split == null || split.Length == 1)
            {
                return string.Empty;
            }
            
            return this.Exist(split[1]) ? split[1] : string.Empty;
        }

        public void NavigateTo(string url, string lang, bool forceLoad = true)
        {
            this.navigationManager.NavigateTo($"/{lang}/{url}", forceLoad: forceLoad);
        }

        public void NavigateToDefault(string lang = "", bool forceLoad = true)
        {
            lang = string.IsNullOrEmpty(lang) ? this.languageOptions.TwoLetterISODefaultLanguage : lang;
            this.navigationManager.NavigateTo($"/{lang}/langChanged", forceLoad: forceLoad);
        }

        public string GetLanguageUrl(string lang, string url)
        {
            return !string.IsNullOrWhiteSpace(url) ? $"{lang}/{url}" : lang;
        }

        public void SetLanguage(string lang)
        {
            if (CultureInfo.CurrentCulture != new CultureInfo(lang))
            {
                var js = (IJSInProcessRuntime)this.jSRuntime;
                js.InvokeVoid("blazorCulture.set", lang);
                this.NavigateToDefault(lang);
            }
        }
    }
}