namespace Sasw.EasyContent.Services
{
    using Contracts.Models;
    using Contracts.Repositories;
    using Contracts.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogQueryService
        : IBlogQueryService
    {
        private readonly IPostSummaryRepository _postSummaryRepository;
        private readonly IPostRepository _postRepository;

        public BlogQueryService(
            IPostSummaryRepository postSummaryRepository,
            IPostRepository postRepository)
        {
            _postSummaryRepository = postSummaryRepository;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<IPostSummary>> GetPostSummaries(IPostFilter postFilter)
        {
            var allPostMetadata = await _postSummaryRepository.GetPostSummaries();

            var query = allPostMetadata.AsQueryable();

            var filterTag = postFilter.Tag;
            if (!string.IsNullOrWhiteSpace(filterTag))
            {
                query = query.Where(x => x.Tags.Contains(filterTag, StringComparer.InvariantCultureIgnoreCase));
            }

            var filterAuthor = postFilter.Author;
            if (!string.IsNullOrWhiteSpace(filterAuthor))
            {
                query = query.Where(x => x.Author.Equals(filterAuthor, StringComparison.InvariantCultureIgnoreCase));
            }

            var filterYear = postFilter.Year;
            if (filterYear.HasValue)
            {
                query = query.Where(x => x.PublishedOn.Year == filterYear.Value);
            }

            var filterMonth = postFilter.Month;
            if (filterMonth.HasValue)
            {
                query = query.Where(x => x.PublishedOn.Month == filterMonth.Value);
            }

            var filterLanguageCode = postFilter.LanguageCode;
            if (!string.IsNullOrWhiteSpace(filterLanguageCode))
            {
                query = query.Where(x => x.LanguageCode.Equals(filterLanguageCode, StringComparison.InvariantCultureIgnoreCase));
            }

            var filterIncludesDraft = postFilter.IncludesDraft;
            query = query.Where(x => x.IsDraft == filterIncludesDraft);

            var postSummaries = query.ToList();
            return postSummaries;
        }

        public async Task<IPost> GetPost(string relativePath)
        {
            var post = await _postRepository.GetPost(relativePath);
            return post;
        }
    }
}
