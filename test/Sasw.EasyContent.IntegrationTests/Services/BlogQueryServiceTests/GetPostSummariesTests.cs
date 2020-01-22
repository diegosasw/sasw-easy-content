namespace Sasw.EasyContent.IntegrationTests.Services.BlogQueryServiceTests
{
    using Builders;
    using Contracts.Configurations;
    using Contracts.Models;
    using Contracts.Services;
    using EasyContent.IoCC;
    using EasyContent.IoCC.Options;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sasw.TestSupport;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TestSupport.Configurations;
    using TestSupport.IoCC;
    using Xunit;

    public static class GetPostSummariesTests
    {
        public class Given_Three_Posts_Available_And_No_Filter_When_Getting_Summary_Posts
            : Given_WhenAsync_Then_Test
        {
            private IBlogQueryService _sut;
            private IEnumerable<IPostSummary> _result;
            private IPostFilter _postFilter;

            protected override void Given()
            {
                var serviceProvider =
                    new ServiceCollection()
                        .AddEasyContent(
                            sp => new PostOptions(),
                            sp => new FileProviderOptions())
                        .OverrideWith(
                            serviceCollection =>
                            {
                                serviceCollection.Replace(ServiceDescriptor.Singleton(typeof(IFileProviderConfiguration), typeof(TestFileProviderConfiguration)));
                                serviceCollection.Replace(ServiceDescriptor.Singleton(typeof(IPostConfiguration), typeof(TestPostConfiguration)));
                                return serviceCollection;
                            })
                        .BuildServiceProvider();

                _postFilter =
                    new PostFilterBuilder()
                        .Build();

                _sut = serviceProvider.GetService<IBlogQueryService>();
            }

            protected override async Task WhenAsync()
            {
                _result = await _sut.GetPostSummaries(_postFilter);
            }

            [Fact]
            public void Then_It_Should_Return_Three_Post_Summaries()
            {
                _result.Should().HaveCount(3);
            }
        }
    }
}