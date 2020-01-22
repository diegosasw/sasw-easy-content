namespace Sasw.EasyContent.IntegrationTests.TestSupport.Configurations
{
    using Contracts.Configurations;
    using Microsoft.Extensions.FileProviders;
    using System;

    public class TestFileProviderConfiguration
        : IFileProviderConfiguration
    {
        public IFileProvider FileProvider { get; set; } = new PhysicalFileProvider(Environment.CurrentDirectory);
    }
}
