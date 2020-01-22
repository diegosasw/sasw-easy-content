namespace Sasw.EasyContent.UnitTests.Services.BlogQueryServiceTests
{
    using Builders;
    using Contracts.Models;
    using Contracts.Repositories;
    using EasyContent.Services;
    using FluentAssertions;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TestSupport;
    using Xunit;

    public static class GetPostSummariesTests
    {
        public class Given_A_Default_Filter_When_Getting_Post_Summaries
            : Given_WhenAsync_Then_Test
        {
            private BlogQueryService _sut;
            private Mock<IPostSummaryRepository> _postSummaryRepositoryMock;
            private Mock<IPostRepository> _postRepositoryMock;
            private IPostFilter _postFilter;
            private IEnumerable<IPostSummary> _result;
            private IEnumerable<IPostSummary> _expectedPosts;

            protected override void Given()
            {
                var postOne = Mock.Of<IPostSummary>();
                var postTwo = Mock.Of<IPostSummary>();
                var postThree = Mock.Of<IPostSummary>();
                var postSummaries =
                    new List<IPostSummary>
                    {
                        postOne,
                        postTwo,
                        postThree
                    };

                _postSummaryRepositoryMock = new Mock<IPostSummaryRepository>();
                _postSummaryRepositoryMock
                    .Setup(x => x.GetPostSummaries())
                    .ReturnsAsync(postSummaries);
                var postSummaryRepository = _postSummaryRepositoryMock.Object;

                _postRepositoryMock = new Mock<IPostRepository>();
                var postRepository = _postRepositoryMock.Object;

                _postFilter = new PostFilterBuilder().Build();

                _sut = new BlogQueryService(postSummaryRepository, postRepository);

                _expectedPosts = postSummaries;
            }

            protected override async Task WhenAsync()
            {
                _result = await _sut.GetPostSummaries(_postFilter);
            }

            [Fact]
            public void Then_It_Should_Return_A_Valid_Result()
            {
                _result.Should().NotBeNull();
            }

            [Fact]
            public void Then_It_Should_Not_Get_The_Post()
            {
                _postRepositoryMock.Verify(x => x.GetPost(It.IsAny<string>()), Times.Never);
            }

            [Fact]
            public void Then_It_Should_Return_The_Same_Posts()
            {
                _result.Should().BeEquivalentTo(_expectedPosts);
            }
        }
    }
}