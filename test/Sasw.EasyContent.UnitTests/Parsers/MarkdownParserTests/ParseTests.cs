namespace Sasw.EasyContent.UnitTests.Parsers.MarkdownParserTests
{
    using EasyContent.Parsers;
    using FluentAssertions;
    using TestSupport;
    using Xunit;

    public static class ParseTests
    {
        public class Given_Markdown_Paragraph_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "This is a paragraph";

                _sut = new MarkdownParser();

                _expectedResult = $"<p>{_markdown}</p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_Paragraph_With_FrontMatter_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                var text = "This is a paragraph";
                _markdown = "---\n" +
                            "title: whatever\n" +
                            "summary: it is ignored\n" +
                            "---\n" +
                            text;

                _sut = new MarkdownParser();

                _expectedResult = $"<p>{text}</p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_Ignoring_FrontMatter()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_Paragraph_With_Inline_FrontMatter_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "---title: whatever\n" +
                            "summary: it is ignored---\n" +
                            "This is a paragraph";

                _sut = new MarkdownParser();

                _expectedResult = $"<p>{_markdown}</p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_Including_FrontMatter_Within()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_Paragraph_With_Custom_Id_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "This is a paragraph {#custom-paragraph-id}";

                _sut = new MarkdownParser();

                _expectedResult = "<p id=\"custom-paragraph-id\">This is a paragraph </p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Custom_Id()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_Paragraph_With_CustomClass_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "This is a paragraph {.custom-paragraph-class}";

                _sut = new MarkdownParser();

                _expectedResult = "<p class=\"custom-paragraph-class\">This is a paragraph </p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Custom_Class()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Header_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "# This is an h1";

                _sut = new MarkdownParser();

                _expectedResult = "<h1 id=\"this-is-an-h1\">This is an h1</h1>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_H1_With_Auto_Identifier()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Header_With_Custom_Id_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "# This is an h1 {#custom-id}";

                _sut = new MarkdownParser();

                _expectedResult = "<h1 id=\"custom-id\">This is an h1</h1>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_H1_With_The_Custom_Id()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Header_With_Custom_Class_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "# This is an h1 {.custom-class}";

                _sut = new MarkdownParser();

                _expectedResult = "<h1 id=\"this-is-an-h1\" class=\"custom-class\">This is an h1</h1>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_H1_With_The_Custom_Class()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Line_Code_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "`This is code` in line";

                _sut = new MarkdownParser();

                _expectedResult = "<p><code>This is code</code> in line</p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_expected_Html_Paragraph_With_The_Code()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Block_Code_When_Converting_To_Html
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                _markdown = "~~~\n" +
                            "This is code\n" +
                            "~~~\n" +
                            "in block";

                _sut = new MarkdownParser();

                _expectedResult = "<pre><code>This is code\n</code></pre>\n<p>in block</p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Code()
            {
                _result.Should().Be(_expectedResult);
            }
        }

        public class Given_Markdown_With_Image_Base64_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                const string imageData = "data:image/svg+xml;base64,WHATEVER";

                _markdown = $"![ImagePath Alt]({imageData})";

                _sut = new MarkdownParser();

                _expectedResult = $"<p><img src=\"{imageData}\" alt=\"ImagePath Alt\" /></p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Image()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_With_Image_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                const string imageData = "sample.png";

                _markdown = $"![ImagePath Alt]({imageData})";

                _sut = new MarkdownParser();

                _expectedResult = $"<p><img src=\"{imageData}\" alt=\"ImagePath Alt\" /></p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Embedded_Image()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }

        public class Given_Markdown_With_Absolute_Image_When_Parsing
            : Given_When_Then_Test
        {
            private MarkdownParser _sut;
            private string _markdown;
            private string _result;
            private string _expectedResult;

            protected override void Given()
            {
                const string imageData = "http://foo/sample.png";

                _markdown = $"![ImagePath Alt]({imageData})";

                _sut = new MarkdownParser();

                _expectedResult = $"<p><img src=\"{imageData}\" alt=\"ImagePath Alt\" /></p>";
            }

            protected override void When()
            {
                _result = _sut.Parse(_markdown);
            }

            [Fact]
            public void Then_It_Should_Return_The_Expected_Html_Paragraph_With_The_Embedded_Image()
            {
                _result.Should().BeEquivalentTo(_expectedResult);
            }
        }
    }
}