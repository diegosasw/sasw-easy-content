namespace Sasw.EasyContent.Contracts.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogQueryService
    {
        Task<IEnumerable<IPostSummary>> GetPostSummaries(IPostFilter postFilter);
        Task<IPost> GetPost(string relativePath);
    }
}