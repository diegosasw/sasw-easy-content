namespace Sasw.EasyContent.Contracts.Parsers
{
    using Models;

    public interface IFrontMatterParser
    {
        IPostMetadata Parse(string frontMatter);
    }
}