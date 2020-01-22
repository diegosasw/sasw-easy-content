namespace Sasw.EasyContent.Repositories
{
    using Contracts.Configurations;
    using Contracts.Repositories;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PostPathRepository
        : IPostPathRepository
    {
        private readonly IPostConfiguration _postConfiguration;
        private readonly IFileProviderConfiguration _fileProviderConfiguration;

        public PostPathRepository(
            IPostConfiguration postConfiguration,
            IFileProviderConfiguration fileProviderConfiguration)
        {
            _fileProviderConfiguration = fileProviderConfiguration;
            _postConfiguration = postConfiguration;
        }

        public IEnumerable<string> GetAllPostFullPaths()
        {
            var postExtension = _postConfiguration.PostExtension;
            var folderPhysicalPath = GetFolderPostsPhysicalPath();
            var filePaths = Directory.EnumerateFiles(folderPhysicalPath, $"*{postExtension}", SearchOption.AllDirectories);
            return filePaths;
        }

        public string GetPostFullPath(string relativeLink)
        {
            var postExtension = _postConfiguration.PostExtension;
            var postRootFolder = _postConfiguration.PostRootFolder;

            var folderPhysicalPath = GetFolderPostsPhysicalPath();
            var folderPhysicalPathTrimmed = folderPhysicalPath.TrimEnd('/');

            var relativeLinkWithoutFolder = relativeLink.Replace(postRootFolder, string.Empty);
            var relativeLinkWithoutFolderTrimmed = relativeLinkWithoutFolder.TrimStart('/');
            var postPath = $"{folderPhysicalPathTrimmed}/{relativeLinkWithoutFolderTrimmed}";

            if (!postPath.EndsWith(postExtension))
            {
                postPath += postExtension;
            }

            return postPath;
        }

        public string GetPostRelativePath(string fullPath)
        {
            var postRootFolder = _postConfiguration.PostRootFolder;
            var postExtension = _postConfiguration.PostExtension;
            var postRootFolderWithoutSeparator = postRootFolder.Replace("/", string.Empty);
            var index = fullPath.IndexOf(postRootFolderWithoutSeparator, StringComparison.InvariantCultureIgnoreCase);
            var path = fullPath.Substring(index);
            var pathAsUrl = path.Replace("\\", "/");
            var pathAsUrlWithoutExtension = pathAsUrl.Replace(postExtension, string.Empty);
            var result = $"/{pathAsUrlWithoutExtension}";
            return result;
        }

        private string GetFolderPostsPhysicalPath()
        {
            var postRootFolder = _postConfiguration.PostRootFolder;
            var fileProvider = _fileProviderConfiguration.FileProvider;
            var directory =
                fileProvider
                    .GetFileInfo(postRootFolder);
            var folderPhysicalPath = directory.PhysicalPath;
            return folderPhysicalPath;
        }
    }
}
