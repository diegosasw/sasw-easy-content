namespace Sasw.EasyContent.Contracts.Services
{
    using Models;

    public interface IPostParserService
    {
        IPostMetadata GetPostMetadata(string content);
        IPost GetPost(string content);
    }
}