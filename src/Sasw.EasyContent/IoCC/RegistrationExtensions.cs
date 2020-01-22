namespace Sasw.EasyContent.IoCC
{
    using Configurations;
    using Contracts.Configurations;
    using Contracts.Parsers;
    using Contracts.Repositories;
    using Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Options;
    using Parsers;
    using Repositories;
    using Services;
    using System;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddEasyContent(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, FileProviderOptions> fileProviderOptionsRetriever)
        {
            PostOptions DefaultOptions(IServiceProvider sp) => PostOptions.Default;
            return AddEasyContent(serviceCollection, DefaultOptions, fileProviderOptionsRetriever);
        }

        public static IServiceCollection AddEasyContent(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, PostOptions> postOptionsRetriever,
            Func<IServiceProvider, FileProviderOptions> fileProviderOptionsRetriever)
        {
            serviceCollection.AddSingleton<IFrontMatterParser, FrontMatterParser>();
            serviceCollection.AddSingleton<IMarkdownParser, MarkdownParser>();
            serviceCollection.AddSingleton<IPostParserService, PostParserService>();
            serviceCollection.AddSingleton<IPostSummaryRepository, PostSummaryRepository>();
            serviceCollection.AddSingleton<IPostRepository, PostRepository>();
            serviceCollection.AddSingleton<IPostPathRepository, PostPathRepository>();
            serviceCollection.AddSingleton<IPostConfiguration>(
                serviceProvider =>
                {
                    var postOptions = postOptionsRetriever.Invoke(serviceProvider);
                    var postRootFolder = postOptions.PostRootFolder;
                    var postExtension = postOptions.PostExtension;
                    var postViewerRoute = postOptions.PostViewerRoute;
                    var postConfiguration = new PostConfiguration(postRootFolder, postExtension, postViewerRoute);
                    return postConfiguration;
                });
            serviceCollection.AddSingleton<IFileProviderConfiguration>(
                serviceProvider =>
                {
                    var fileProviderOptions = fileProviderOptionsRetriever.Invoke(serviceProvider);
                    var fileProvider = fileProviderOptions.FileProvider;
                    var fileProviderConfiguration = new FileProviderConfiguration(fileProvider);
                    return fileProviderConfiguration;
                });
            serviceCollection.AddSingleton<IBlogQueryService, BlogQueryService>();

            return serviceCollection;
        }
    }
}