namespace Sasw.EasyContent.Parsers
{
    using Contracts.Parsers;
    using Markdig;
    using System;

    public class MarkdownParser
        : IMarkdownParser
    {
        public string Parse(string markdown)
        {
            if (markdown is null)
            {
                throw new ArgumentNullException(nameof(markdown));
            }
            var pipeline =
                new MarkdownPipelineBuilder()
                    .UseAdvancedExtensions()
                    .UseYamlFrontMatter()
                    .Build();

            var html = Markdown.ToHtml(markdown, pipeline).Trim();
            return html;
        }
    }
}
