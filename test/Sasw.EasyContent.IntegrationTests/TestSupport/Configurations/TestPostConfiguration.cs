namespace Sasw.EasyContent.IntegrationTests.TestSupport.Configurations
{
    using Contracts.Configurations;

    public class TestPostConfiguration
        : IPostConfiguration
    {
        public string PostRootFolder { get; set; } = "/TestSupport";
        public string PostExtension { get; set; } = ".md";
        public string PostViewerRoute { get; set; } = "/Foo";
    }
}
