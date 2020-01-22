namespace Sasw.EasyContent.Configurations
{
    using Contracts.Configurations;
    using Microsoft.Extensions.FileProviders;

    internal sealed class FileProviderConfiguration
        : IFileProviderConfiguration
    {
        public IFileProvider FileProvider { get; }

        public FileProviderConfiguration(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }
    }
}
