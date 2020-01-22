namespace Sasw.EasyContent.Repositories
{
    using Contracts.Models;
    using Contracts.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostSummaryCacheRepository
        : IPostSummaryRepository
    {
        private readonly IPostSummaryRepository _postSummaryRepository;
        private readonly IList<IPostSummary> _postSummaries;

        public PostSummaryCacheRepository(IPostSummaryRepository postSummaryRepository)
        {
            _postSummaryRepository = postSummaryRepository;
            _postSummaries = new List<IPostSummary>();
        }

        public async Task<IEnumerable<IPostSummary>> GetPostSummaries()
        {
            var isEmpty = !_postSummaries.Any();
            if (isEmpty)
            {
                var postSummaries = await _postSummaryRepository.GetPostSummaries();
                foreach (var postSummary in postSummaries)
                {
                    _postSummaries.Add(postSummary);
                }
            }

            return _postSummaries;
        }
    }
}
