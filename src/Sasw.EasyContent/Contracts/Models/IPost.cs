namespace Sasw.EasyContent.Contracts.Models
{
    public interface IPost
        : IPostMetadata
    {
        string Content { get; }
    }
}