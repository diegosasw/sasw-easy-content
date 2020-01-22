namespace Sasw.EasyContent.Contracts.Configurations
{
    using Microsoft.Extensions.FileProviders;

    public interface IFileProviderConfiguration
    {
        IFileProvider FileProvider { get; }
    }
}