namespace BlazorTutorial.Helpers
{
    public static class CultureHelper
    {
        public static string DefaultLangauge { get; } = "de";

        public static List<string> Languages { get; } = new List<string>
        {
            "en",
            "de"
        };

        public static string GetLanguage(string lang)
            => lang != null ? Languages.FirstOrDefault(x => x.Equals(lang, StringComparison.OrdinalIgnoreCase)) ?? string.Empty : string.Empty;
    }
}
