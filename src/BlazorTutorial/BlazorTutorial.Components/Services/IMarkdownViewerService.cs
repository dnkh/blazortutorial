namespace BlazorTutorial.Components.Services
{
    internal interface IMarkdownViewerService
    {
        public Task<List<string>> GetAllMarkdownFileNames();
    }
}
