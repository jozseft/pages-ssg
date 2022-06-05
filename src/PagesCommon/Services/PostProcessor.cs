using Markdig;
using PagesCommon.Interfaces;

namespace PagesCommon.Services
{
    public class PostProcessor : IPostProcessor
    {
        public string GetHtmlByMarkdown(string markdownText)
        {
            return Markdown.ToHtml(markdownText).Trim();
        }
    }
}
