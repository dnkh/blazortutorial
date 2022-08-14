namespace BlazorTutorial.Language
{
    public class LanguageOptions
    {
        private const string _default = "de";

        /// <summary>
        /// Add TwoLetterISO default language when language not found. (default: de)
        /// </summary>
        public string TwoLetterISODefaultLanguage { get; init; } = _default;

        /// <summary>
        /// Add allowed TwoLetterISO languages
        /// </summary>
        public string[] TwoLetterISOLanguages { get; init; } = new[] { _default };

        /// <summary>
        /// Get all accepted languages
        /// </summary>
        /// <returns>Array of accepted languages; otherwise, empty array</returns>
        public string[] GetLanguages() => TwoLetterISOLanguages ?? Array.Empty<string>();
    }
}
