namespace Sasw.EasyContent.Configurations
{
    using Contracts.Configurations;
    using System;

    internal sealed class PostConfiguration
        : IPostConfiguration
    {
        public string PostRootFolder { get; }
        public string PostExtension { get; }
        public string PostViewerRoute { get; }

        public PostConfiguration(string postsRootFolder, string postsExtension, string postViewerRoute)
        {
            PostRootFolder = postsRootFolder ?? throw new ArgumentNullException(nameof(postsRootFolder));
            PostExtension = postsExtension ?? throw new ArgumentNullException(nameof(postsExtension));
            PostViewerRoute = postViewerRoute ?? throw new ArgumentNullException(nameof(postViewerRoute));
        }
    }
}
