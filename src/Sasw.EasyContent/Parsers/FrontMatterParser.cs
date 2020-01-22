namespace Sasw.EasyContent.Parsers
{
    using Contracts.Models;
    using Contracts.Parsers;
    using Exceptions;
    using Models;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class FrontMatterParser
        : IFrontMatterParser
    {
        public IPostMetadata Parse(string frontMatter)
        {
            var yamlDeserializer =
                new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .IgnoreUnmatchedProperties()
                    .Build();

            var post = yamlDeserializer.Deserialize<PostMetadata>(frontMatter);

            if (post is null)
            {
                throw new ParsingException($"Could not deserialize front matter {frontMatter}. Posts require metadata surrounded by '---'.");
            }

            return post;
        }
    }
}
