using Markdig;
using Microsoft.AspNetCore.Components;

namespace BlazorTutorial.Pages
{
    public partial class EditMarkdownBase : ComponentBase
    {
        public string Body { get; set; } = string.Empty;
        public string Preview => Markdown.ToHtml(Body);
    }
}