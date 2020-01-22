namespace Sasw.EasyContent.Repositories
{
    using Contracts.Models;
    using Contracts.Repositories;
    using Contracts.Services;
    using Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostSummaryRepository
        : IPostSummaryRepository
    {
        private readonly IPostPathRepository _postPathRepository;
        private readonly IPostParserService _postParserService;

        public PostSummaryRepository(
            IPostPathRepository postPathRepository,
            IPostParserService postParserService)
        {
            _postPathRepository = postPathRepository;
            _postParserService = postParserService;
        }

        public async Task<IEnumerable<IPostSummary>> GetPostSummaries()
        {
            var postFullPaths = _postPathRepository.GetAllPostFullPaths();

            var postSummaries = new List<IPostSummary>();

            foreach (var postFullPath in postFullPaths)
            {
                var content = await File.ReadAllTextAsync(postFullPath);
                var postMetadata = _postParserService.GetPostMetadata(content);
                var relativePath = _postPathRepository.GetPostRelativePath(postFullPath);
                var postSummary = new PostSummary(postMetadata, relativePath);
                postSummaries.Add(postSummary);
            }

            return postSummaries.OrderBy(x => x.PublishedOn).ToList();
        }
    }
}
