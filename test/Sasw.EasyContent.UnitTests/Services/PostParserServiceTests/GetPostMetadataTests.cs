namespace Sasw.EasyContent.UnitTests.Services.PostParserServiceTests
{
    using Contracts.Models;
    using Contracts.Parsers;
    using EasyContent.Services;
    using FluentAssertions;
    using Moq;
    using TestSupport;
    using Xunit;

    public static class GetPostMetadataTests
    {
        public class Given_Content_With_Front_Matter_When_Getting_Post_Metadata
            : Given_When_Then_Test
        {
            private PostParserService _sut;
            private Mock<IFrontMatterParser> _frontMatterParserMock;
            private Mock<IMarkdownParser> _markdownParserMock;
            private string _content;
            private IPostMetadata _result;
            private string _expectedYaml;
            private IPostMetadata _expectedResult;

            protected override void Given()
            {
                var yml =
                    "title: foo\n" +
                    "summary: bar\n" +
                    "tags: [tag1, tag2, tag3]\n";

                var postMetadata = Mock.Of<IPostMetadata>();
                _frontMatterParserMock = new Mock<IFrontMatterParser>();
                _frontMatterParserMock
                    .Setup(x => x.Parse(yml))
                    .Returns(postMetadata);

                _content = "---\n" +
                           yml +
                           "---\n" +
                           "This is some ignored markdown content \n";

                _markdownParserMock = new Mock<IMarkdownParser>();

                var frontMatterParser = _frontMatterParserMock.Object;
                var markdownParser = _markdownParserMock.Object;

                _sut = new PostParserService(frontMatterParser, markdownParser);

                _expectedYaml = yml;
                _expectedResult = postMetadata;
            }

            protected override void When()
            {
                _result = _sut.GetPostMetadata(_content);
            }

            [Fact]
            public void Then_It_Should_Use_Front_Matter_Parser_For_Yaml_Content()
            {
                _frontMatterParserMock.Verify(x => x.Parse(_expectedYaml));
            }

            [Fact]
            public void Then_It_Should_Not_Use_Markdown_Parser()
            {
                _markdownParserMock.Verify(x => x.Parse(_content), Times.Never);
            }

            [Fact]
            public void Then_It_Should_Return_Whatever_FrontMatter_Parser_Returns()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Content_With_Front_Matter_Duplicated_When_Getting_Post_Metadata
            : Given_When_Then_Test
        {
            private PostParserService _sut;
            private Mock<IFrontMatterParser> _frontMatterParserMock;
            private Mock<IMarkdownParser> _markdownParserMock;
            private string _content;
            private IPostMetadata _result;
            private string _expectedYaml;
            private IPostMetadata _expectedResult;

            protected override void Given()
            {
                var yml =
                    "title: foo\n" +
                    "summary: bar\n" +
                    "tags: [tag1, tag2, tag3]\n";

                var postMetadata = Mock.Of<IPostMetadata>();
                _frontMatterParserMock = new Mock<IFrontMatterParser>();
                _frontMatterParserMock
                    .Setup(x => x.Parse(yml))
                    .Returns(postMetadata);

                _content = "---\n" +
                           yml +
                           "---\n" +
                           "This is some ignored markdown content \n" +
                           "---\n" +
                           yml +
                           "---\n" +
                           "This is again some ignored markdown content \n";

                _markdownParserMock = new Mock<IMarkdownParser>();

                var frontMatterParser = _frontMatterParserMock.Object;
                var markdownParser = _markdownParserMock.Object;

                _sut = new PostParserService(frontMatterParser, markdownParser);

                _expectedYaml = yml;
                _expectedResult = postMetadata;
            }

            protected override void When()
            {
                _result = _sut.GetPostMetadata(_content);
            }

            [Fact]
            public void Then_It_Should_Use_Front_Matter_Parser_For_Yaml_Content()
            {
                _frontMatterParserMock.Verify(x => x.Parse(_expectedYaml));
            }

            [Fact]
            public void Then_It_Should_Not_Use_Markdown_Parser()
            {
                _markdownParserMock.Verify(x => x.Parse(_content), Times.Never);
            }

            [Fact]
            public void Then_It_Should_Return_Whatever_FrontMatter_Parser_Returns()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Content_With_Front_Matter_With_Empty_Lines_Between_Properties_When_Getting_Post_Metadata
            : Given_When_Then_Test
        {
            private PostParserService _sut;
            private Mock<IFrontMatterParser> _frontMatterParserMock;
            private Mock<IMarkdownParser> _markdownParserMock;
            private string _content;
            private IPostMetadata _result;
            private string _expectedYaml;
            private IPostMetadata _expectedResult;

            protected override void Given()
            {
                var yml =
                    "title: foo \n\n\n\n" +
                    "summary: bar \n" +
                    "tags: [tag1, tag2, tag3]\n";

                var postMetadata = Mock.Of<IPostMetadata>();
                _frontMatterParserMock = new Mock<IFrontMatterParser>();
                _frontMatterParserMock
                    .Setup(x => x.Parse(yml))
                    .Returns(postMetadata);

                _content = "---\n" +
                           yml +
                           "---\n" +
                           "This is some ignored markdown content \n";

                _markdownParserMock = new Mock<IMarkdownParser>();

                var frontMatterParser = _frontMatterParserMock.Object;
                var markdownParser = _markdownParserMock.Object;

                _sut = new PostParserService(frontMatterParser, markdownParser);

                _expectedYaml = yml;
                _expectedResult = postMetadata;
            }

            protected override void When()
            {
                _result = _sut.GetPostMetadata(_content);
            }

            [Fact]
            public void Then_It_Should_Use_Front_Matter_Parser_For_Yaml_Content()
            {
                _frontMatterParserMock.Verify(x => x.Parse(_expectedYaml));
            }

            [Fact]
            public void Then_It_Should_Not_Use_Markdown_Parser()
            {
                _markdownParserMock.Verify(x => x.Parse(_content), Times.Never);
            }

            [Fact]
            public void Then_It_Should_Return_Whatever_FrontMatter_Parser_Returns()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Content_With_Front_Matter_Starting_After_Empty_Lines_And_Tabs_And_Returns_When_Getting_Post_Metadata
            : Given_When_Then_Test
        {
            private PostParserService _sut;
            private Mock<IFrontMatterParser> _frontMatterParserMock;
            private Mock<IMarkdownParser> _markdownParserMock;
            private string _content;
            private IPostMetadata _result;
            private string _expectedYaml;
            private IPostMetadata _expectedResult;

            protected override void Given()
            {
                var yml =
                    "title: foo \n" +
                    "summary: bar \n" +
                    "tags: [tag1, tag2, tag3]\n";

                var postMetadata = Mock.Of<IPostMetadata>();
                _frontMatterParserMock = new Mock<IFrontMatterParser>();
                _frontMatterParserMock
                    .Setup(x => x.Parse(yml))
                    .Returns(postMetadata);

                _content = "\n\n\r\r\n\t\n\t\t\t\n---\n" +
                           yml +
                           "---\n\t\r\n" +
                           "This is some ignored markdown content \n";

                _markdownParserMock = new Mock<IMarkdownParser>();

                var frontMatterParser = _frontMatterParserMock.Object;
                var markdownParser = _markdownParserMock.Object;

                _sut = new PostParserService(frontMatterParser, markdownParser);

                _expectedYaml = yml;
                _expectedResult = postMetadata;
            }

            protected override void When()
            {
                _result = _sut.GetPostMetadata(_content);
            }

            [Fact]
            public void Then_It_Should_Use_Front_Matter_Parser_For_Yaml_Content()
            {
                _frontMatterParserMock.Verify(x => x.Parse(_expectedYaml));
            }

            [Fact]
            public void Then_It_Should_Not_Use_Markdown_Parser()
            {
                _markdownParserMock.Verify(x => x.Parse(_content), Times.Never);
            }

            [Fact]
            public void Then_It_Should_Return_Whatever_FrontMatter_Parser_Returns()
            {
                _result.Should().Be(_expectedResult);
            }
        }
    }
}