namespace Sasw.EasyContent.UnitTests.Repositories.PostPathRepositoryTests
{
    using Contracts.Configurations;
    using EasyContent.Repositories;
    using FluentAssertions;
    using Moq;
    using TestSupport;
    using Xunit;

    public static class GetPostRelativePathTests
    {
        public class Given_A_Full_Windows_Path_When_Getting_Post_Relative_Path
            : Given_When_Then_Test
        {
            private PostPathRepository _sut;
            private Mock<IPostConfiguration> _postConfigurationMock;
            private string _fullPath;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _postConfigurationMock = new Mock<IPostConfiguration>();
                _postConfigurationMock
                    .Setup(x => x.PostRootFolder)
                    .Returns("/posts");
                _postConfigurationMock
                    .Setup(x => x.PostExtension)
                    .Returns(".md");
                var postConfiguration = _postConfigurationMock.Object;

                var fileProviderConfiguration = Mock.Of<IFileProviderConfiguration>();

                _fullPath = "d:\\src\\sasw\\sasw-website\\src\\Sasw.Website\\wwwroot\\posts\\foo\\bar\\sample.md";

                _sut = new PostPathRepository(postConfiguration, fileProviderConfiguration);

                _expectedResult = "/posts/foo/bar/sample";
            }

            protected override void When()
            {
                _result = _sut.GetPostRelativePath(_fullPath);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Relative_Path_Without_Extension()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_A_Full_Unix_Path_When_Getting_Post_Relative_Path
            : Given_When_Then_Test
        {
            private PostPathRepository _sut;
            private Mock<IPostConfiguration> _postConfigurationMock;
            private string _fullPath;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _postConfigurationMock = new Mock<IPostConfiguration>();
                _postConfigurationMock
                    .Setup(x => x.PostRootFolder)
                    .Returns("/posts");
                _postConfigurationMock
                    .Setup(x => x.PostExtension)
                    .Returns(".md");
                var postConfiguration = _postConfigurationMock.Object;

                var fileProviderConfiguration = Mock.Of<IFileProviderConfiguration>();

                _fullPath = "/d/src/sasw/sasw-website/src/Sasw.Website/wwwroot/posts/foo/bar/sample.md";

                _sut = new PostPathRepository(postConfiguration, fileProviderConfiguration);

                _expectedResult = "/posts/foo/bar/sample";
            }

            protected override void When()
            {
                _result = _sut.GetPostRelativePath(_fullPath);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Relative_Path_Without_Extension()
            {
                _result.Should().Be(_expectedResult);
            }
        }
    }
}