namespace Sasw.EasyContent.Contracts.Repositories
{
    using Models;
    using System.Threading.Tasks;

    public interface IPostRepository
    {
        Task<IPost> GetPost(string relativePath);
    }
}