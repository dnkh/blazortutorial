using BlazorTutorial.Components.Classes;
using BlazorTutorial.Language;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace BlazorTutorial.Components.Services
{
    using System.Net.Http.Json;

    internal class MarkdownViewerService : IMarkdownViewerService
    {
        private readonly HttpClient _httpClient;

        public MarkdownViewerService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<string>> GetAllMarkdownFileNames()
        {
           var files =  await _httpClient.GetFromJsonAsync<string[]>("https://blazortutorialbackendfunctions.azurewebsites.net/api/getAllMarkdownFileNames");

           return files.ToList();
        }
    }
}