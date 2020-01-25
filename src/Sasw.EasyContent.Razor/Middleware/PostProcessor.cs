namespace Sasw.EasyContent.Razor.Middleware
{
    using Configurations;
    using Contracts.Configurations;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class PostProcessor
    {
        private readonly RequestDelegate _next;
        private readonly IPostConfiguration _postConfiguration;

        public PostProcessor(
            RequestDelegate next,
            IPostConfiguration postConfiguration)
        {
            _next = next;
            _postConfiguration = postConfiguration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var postsRootFolder = $"{_postConfiguration.PostRootFolder.TrimEnd('/')}/";
            var requestedPath = context.Request.Path.Value;
            var hasExtension = Path.HasExtension(requestedPath);
            var isAsset = hasExtension && !requestedPath.EndsWith(_postConfiguration.PostExtension);
            var isPost = requestedPath.StartsWith(postsRootFolder, StringComparison.InvariantCultureIgnoreCase) && !isAsset;
            if (!isPost)
            {
                await _next(context);
                return;
            }

            context.Items[Constants.RequestedPathParamsKey] = requestedPath;
            var postViewerRoute = _postConfiguration.PostViewerRoute;
            context.Request.Path = postViewerRoute;

            await _next(context);
        }
    }
}
