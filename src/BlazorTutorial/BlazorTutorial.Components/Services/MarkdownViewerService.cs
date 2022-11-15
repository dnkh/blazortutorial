using BlazorTutorial.Language;
using System.Net.Http;

namespace BlazorTutorial.Components.Services
{
    internal class MarkdownViewerService : IMarkdownViewerService
    {
        private readonly HttpClient _httpClient;

        public MarkdownViewerService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetAllMarkdownFileNames()
        {
            HttpResponseMessage response =  await _httpClient.GetAsync("api/getAllMarkdownFileNames");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            /// TODO: return results as a list
            return new List<string> { "TestFile.rtf", "TestFile2.rtf", "TestFile3.rtf", "TestFile4.rtf" };
        }
    }
}