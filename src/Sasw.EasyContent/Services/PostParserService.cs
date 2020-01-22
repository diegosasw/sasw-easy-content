namespace Sasw.EasyContent.Services
{
    using Contracts.Models;
    using Contracts.Parsers;
    using Contracts.Services;
    using Models;
    using System;
    using System.Linq;

    public class PostParserService
        : IPostParserService
    {
        private readonly IFrontMatterParser _frontMatterParser;
        private readonly IMarkdownParser _markdownParser;

        public PostParserService(
            IFrontMatterParser frontMatterParser,
            IMarkdownParser markdownParser)
        {
            _frontMatterParser = frontMatterParser;
            _markdownParser = markdownParser;
        }

        public IPost GetPost(string content)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));

            var postMetadata = GetPostMetadata(content);
            var html = _markdownParser.Parse(content);

            var post = new Post(postMetadata, html);
            return post;
        }

        public IPostMetadata GetPostMetadata(string content)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));

            const string frontMatterDelimiter = "---";
            var sections = content.Split(frontMatterDelimiter);
            var frontMatter = sections.ElementAtOrDefault(1)?.TrimStart('\n', '\r');

            var postMetadata = _frontMatterParser.Parse(frontMatter);
            return postMetadata;
        }
    }
}
