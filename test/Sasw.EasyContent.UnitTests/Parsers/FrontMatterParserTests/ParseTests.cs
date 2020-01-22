namespace Sasw.EasyContent.UnitTests.Parsers.FrontMatterParserTests
{
    using Contracts.Models;
    using EasyContent.Parsers;
    using Exceptions;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestSupport;
    using Xunit;

    public static class ParseTests
    {
        public class Given_A_Stream_With_Yaml_FrontMatter_With_All_Properties_When_Parsing
            : Given_When_Then_Test
        {
            private FrontMatterParser _sut;
            private string _frontMatter;
            private IPostMetadata _result;
            private string _expectedTitle;
            private string _expectedSummary;
            private string _expectedAuthor;
            private string _expectedLanguageCode;
            private IEnumerable<string> _expectedTags;
            private string _expectedImage;
            private string _expectedBasePath;
            private DateTime _expectedPublishedOn;

            protected override void Given()
            {
                var title = "This is the title";
                var summary = "This is the summary";
                var tags = "[tag1,tag2 , tag3,tag4]";
                var author = "Sasw";
                var languageCode = "ES";
                var image = "sample.png";
                var publishedOn = "2020-01-09";
                var basePath = "https://foo/bar";
                _frontMatter =
                    $"title: {title}\n" +
                    $"summary: {summary}\n" +
                    $"tags: {tags}\n" +
                    $"author: {author}\n" +
                    $"languageCode: {languageCode}\n" +
                    $"image: {image}\n" +
                    $"publishedOn: {publishedOn}\n" +
                    $"basePath: {basePath}\n" +
                    "foo: irrelevant";
                _sut = new FrontMatterParser();

                _expectedTitle = title;
                _expectedSummary = summary;
                _expectedAuthor = author;
                _expectedLanguageCode = languageCode;
                _expectedTags =
                    tags
                        .Replace("[", string.Empty)
                        .Replace("]", string.Empty)
                        .Split(",")
                        .Select(x => x.Trim());
                _expectedImage = image;
                _expectedPublishedOn = new DateTime(2020, 1, 9);
                _expectedBasePath = basePath;
            }

            protected override void When()
            {
                _result = _sut.Parse(_frontMatter);
            }

            [Fact]
            public void Then_It_Should_Return_A_Valid_YamlModel()
            {
                _result.Should().NotBeNull();
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Title()
            {
                _result.Title.Should().Be(_expectedTitle);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Summary()
            {
                _result.Summary.Should().Be(_expectedSummary);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Author()
            {
                _result.Author.Should().Be(_expectedAuthor);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_LanguageCode()
            {
                _result.LanguageCode.Should().Be(_expectedLanguageCode);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Tags()
            {
                _result.Tags.Should().BeEquivalentTo(_expectedTags);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Image()
            {
                _result.Image.Should().Be(_expectedImage);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_PublishedOn()
            {
                _result.PublishedOn.Should().BeSameDateAs(_expectedPublishedOn);
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_BasePath()
            {
                _result.BasePath.Should().Be(_expectedBasePath);
            }
        }

        public class Given_A_Stream_With_Empty_Yaml_FrontMatter_When_Parsing
            : Given_When_Then_Test
        {
            private FrontMatterParser _sut;
            private string _frontMatter;
            private ParsingException _exception;

            protected override void Given()
            {
                _frontMatter = "";
                _sut = new FrontMatterParser();
            }

            protected override void When()
            {
                try
                {
                    _sut.Parse(_frontMatter);
                }
                catch (ParsingException exception)
                {
                    _exception = exception;
                }
            }

            [Fact]
            public void Then_It_Should_Throw_A_ParsingException()
            {
                _exception.Should().NotBeNull();
            }
        }

        public class Given_A_Stream_With_Only_Title_In_FrontMatter_When_Parsing
            : Given_When_Then_Test
        {
            private FrontMatterParser _sut;
            private string _frontMatter;
            private IPostMetadata _result;
            private string _expectedTitle;

            protected override void Given()
            {
                _frontMatter = "title: foo";
                _sut = new FrontMatterParser();

                _expectedTitle = "foo";
            }

            protected override void When()
            {
                _result = _sut.Parse(_frontMatter);
            }

            [Fact]
            public void Then_It_Should_Return_A_Valid_YamlModel()
            {
                _result.Should().NotBeNull();
            }

            [Fact]
            public void Then_It_Should_Have_The_Expected_Title()
            {
                _result.Title.Should().Be(_expectedTitle);
            }

            [Fact]
            public void Then_It_Should_Have_Null_Summary()
            {
                _result.Summary.Should().BeNull();
            }

            [Fact]
            public void Then_It_Should_Have_Null_Author()
            {
                _result.Author.Should().BeNull();
            }

            [Fact]
            public void Then_It_Should_Have_Null_LanguageCode()
            {
                _result.LanguageCode.Should().BeNull();
            }

            [Fact]
            public void Then_It_Should_Have_Empty_Tags()
            {
                _result.Tags.Should().BeEmpty();
            }

            [Fact]
            public void Then_It_Should_Have_Null_Image()
            {
                _result.Image.Should().BeNull();
            }

            [Fact]
            public void Then_It_Should_Have_Default_PublishedOn()
            {
                _result.PublishedOn.Should().Be(default);
            }

            [Fact]
            public void Then_It_Should_Have_Null_BasePath()
            {
                _result.BasePath.Should().BeNull();
            }
        }
    }
}