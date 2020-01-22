namespace Sasw.EasyContent.UnitTests.Services.PostParserServiceTests
{
    using Contracts.Models;
    using Contracts.Parsers;
    using EasyContent.Services;
    using FluentAssertions;
    using Moq;
    using System;
    using System.Collections.Generic;
    using TestSupport;
    using Xunit;

    public static class GetPostTests
    {
        public class Given_Content_With_Front_Matter_When_Getting_Post_Metadata
            : Given_When_Then_Test
        {
            private PostParserService _sut;
            private string _content;
            private Mock<IFrontMatterParser> _frontMatterParserMock;
            private Mock<IMarkdownParser> _markdownParserMock;
            private IPost _result;
            private string _expectedYaml;
            private string _expectedTitle;
            private string _expectedAuthor;
            private string _expectedSummary;
            private string _expectedBasePath;
            private List<string> _expectedTags;
            private string _expectedImage;
            private DateTime _expectedPublishedOn;
            private bool _expectedIsDraft;

            protected override void Given()
            {
                var yaml = "foo";

                var postMetadataMock = new Mock<IPostMetadata>();

                var title = "title";
                postMetadataMock
                    .Setup(x => x.Title)
                    .Returns(title);

                var author = "author";
                postMetadataMock
                    .Setup(x => x.Author)
                    .Returns(author);

                var summary = "summary";
                postMetadataMock
                    .Setup(x => x.Summary)
                    .Returns(summary);

                var image = "sample.png";
                postMetadataMock
                    .Setup(x => x.Image)
                    .Returns(image);

                var tags = new List<string> { "tag1", "tag2" };
                postMetadataMock
                    .Setup(x => x.Tags)
                    .Returns(tags);

                var basePath = "basePath";
                postMetadataMock
                    .Setup(x => x.BasePath)
                    .Returns(basePath);

                var publishedOn = new DateTime(2020, 10, 01);
                postMetadataMock
                    .Setup(x => x.PublishedOn)
                    .Returns(publishedOn);

                postMetadataMock
                    .Setup(x => x.IsDraft)
                    .Returns(false);

                var postMetadata = postMetadataMock.Object;

                _frontMatterParserMock = new Mock<IFrontMatterParser>();
                _frontMatterParserMock
                    .Setup(x => x.Parse(yaml))
                    .Returns(postMetadata);

                _content = "---\n" +
                           yaml +
                           "---\n" +
                           "This is some markdown content";

                var html = "<p>This is some markdown content</p>";

                _markdownParserMock = new Mock<IMarkdownParser>();
                _markdownParserMock
                    .Setup(x => x.Parse(_content))
                    .Returns(html);

                var frontMatterParser = _frontMatterParserMock.Object;
                var markdownParser = _markdownParserMock.Object;

                _sut = new PostParserService(frontMatterParser, markdownParser);

                _expectedYaml = yaml;

                _expectedTitle = title;
                _expectedAuthor = author;
                _expectedSummary = summary;
                _expectedBasePath = basePath;
                _expectedTags = tags;
                _expectedImage = image;
                _expectedPublishedOn = publishedOn;
                _expectedIsDraft = false;
            }

            protected override void When()
            {
                _result = _sut.GetPost(_content);
            }

            [Fact]
            public void Then_It_Should_Use_Front_Matter_Parser_For_Yaml_Content()
            {
                _frontMatterParserMock.Verify(x => x.Parse(_expectedYaml));
            }

            [Fact]
            public void Then_It_Should_Use_Markdown_Parser()
            {
                _markdownParserMock.Verify(x => x.Parse(_content));
            }

            [Fact]
            public void Then_It_Should_Return_A_Valid_Result()
            {
                _result.Should().NotBeNull();
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_Title()
            {
                _result.Title.Should().Be(_expectedTitle);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_Author()
            {
                _result.Author.Should().Be(_expectedAuthor);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_Summary()
            {
                _result.Summary.Should().Be(_expectedSummary);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_Tags()
            {
                _result.Tags.Should().BeEquivalentTo(_expectedTags);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_Image()
            {
                _result.Image.Should().Be(_expectedImage);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_PublishedOn()
            {
                _result.PublishedOn.Should().BeSameDateAs(_expectedPublishedOn);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_BasePath()
            {
                _result.BasePath.Should().Be(_expectedBasePath);
            }

            [Fact]
            public void Then_It_Should_Return_A_Post_With_The_Expected_Metadata_IsDraft()
            {
                _result.IsDraft.Should().Be(_expectedIsDraft);
            }
        }
    }
}