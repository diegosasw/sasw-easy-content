namespace Sasw.EasyContent.IntegrationTests.IoCC
{
    using Contracts.Services;
    using EasyContent.IoCC;
    using EasyContent.IoCC.Options;
    using EasyContent.Services;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Sasw.TestSupport;
    using System;
    using Xunit;

    public static class RegistrationTests
    {
        public class Given_A_Dependency_Injection_Container_With_EasyContent_Registered_When_Resolving_IBlogQueryService
            : Given_When_Then_Test
        {
            private IServiceProvider _sut;
            private IBlogQueryService _result;

            protected override void Given()
            {
                _sut =
                    new ServiceCollection()
                        .AddEasyContent(
                            sp => PostOptions.Default,
                            sp => new FileProviderOptions())
                        .BuildServiceProvider();
            }

            protected override void When()
            {
                _result = _sut.GetService<IBlogQueryService>();
            }

            [Fact]
            public void Then_It_Should_Be_A_BlogQueryService()
            {
                _result.Should().BeAssignableTo<BlogQueryService>();
            }
        }
    }
}