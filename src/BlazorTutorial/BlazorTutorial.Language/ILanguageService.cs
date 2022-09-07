namespace BlazorTutorial.Language
{
    public interface ILanguageService
    {
        bool Exist(string lang);

        string GetUriLanguage();

        void NavigateTo(string url, string lang = "", bool forceLoad = true);

        void NavigateToDefault(string lang = "", bool forceLoad = true);

        string GetLanguageUrl(string lang, string url);

        void SetLanguage(string lang);
    }
}