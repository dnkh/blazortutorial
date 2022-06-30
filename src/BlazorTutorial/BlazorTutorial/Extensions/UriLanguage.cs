using BlazorTutorial.Helpers;

namespace BlazorTutorial.Extensions
{
    public static partial class Extension
    {
        public static string UriLanguage(this string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return string.Empty;
            }

            
            var uriBuilder = new UriBuilder(uri);

            var split = uriBuilder.Path.Split("/");
            return split == null || split.Length == 1
                    ? string.Empty
                    : CultureHelper.GetLanguage(split[1]);
        }
    }
}
