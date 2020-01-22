namespace Sasw.EasyContent.Contracts.Models
{
    public interface IPostSummary
        : IPostMetadata
    {
        string RelativePath { get; }
    }
}