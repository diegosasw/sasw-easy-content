namespace Sasw.EasyContent.Contracts.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostSummaryRepository
    {
        Task<IEnumerable<IPostSummary>> GetPostSummaries();
    }
}