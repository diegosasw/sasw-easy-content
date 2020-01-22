namespace Sasw.EasyContent.Repositories
{
    using Contracts.Models;
    using Contracts.Repositories;
    using Contracts.Services;
    using System.IO;
    using System.Threading.Tasks;

    public class PostRepository
        : IPostRepository
    {
        private readonly IPostPathRepository _postPathRepository;
        private readonly IPostParserService _postParserService;

        public PostRepository(
            IPostPathRepository postPathRepository,
            IPostParserService postParserService)
        {
            _postPathRepository = postPathRepository;
            _postParserService = postParserService;
        }

        public async Task<IPost> GetPost(string relativePath)
        {
            var fullPath = _postPathRepository.GetPostFullPath(relativePath);
            var content = await File.ReadAllTextAsync(fullPath);
            var post = _postParserService.GetPost(content);
            return post;
        }
    }
}
