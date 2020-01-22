namespace Sasw.EasyContent.Contracts.Repositories
{
    using System.Collections.Generic;

    public interface IPostPathRepository
    {
        IEnumerable<string> GetAllPostFullPaths();
        string GetPostFullPath(string relativeLink);
        string GetPostRelativePath(string fullPath);
    }
}