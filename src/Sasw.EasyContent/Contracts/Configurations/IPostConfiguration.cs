namespace Sasw.EasyContent.Contracts.Configurations
{
    public interface IPostConfiguration
    {
        string PostRootFolder { get; }
        string PostExtension { get; }
        string PostViewerRoute { get; }
    }
}