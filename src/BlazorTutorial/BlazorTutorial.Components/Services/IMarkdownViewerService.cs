namespace BlazorTutorial.Components.Services
{
    internal interface IMarkdownViewerService
    {
        public Task<IEnumerable<string>> GetAllMarkdownFileNames();
    }
}
