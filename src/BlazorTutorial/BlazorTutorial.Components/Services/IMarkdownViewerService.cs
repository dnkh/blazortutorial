namespace BlazorTutorial.Components.Services
{
    internal interface IMarkdownViewerService
    {
        public Task<List<string>> GetAllMarkdownFileNames(); 
        public Task<string> GetMarkdownFileByName(string name);
    }
}
