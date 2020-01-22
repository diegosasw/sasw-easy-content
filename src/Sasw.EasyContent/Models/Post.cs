namespace Sasw.EasyContent.Models
{
    using Contracts.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Post
        : IPost
    {
        private readonly IPostMetadata _postMetadata;

        public Post(IPostMetadata postMetadata, string content)
        {
            _postMetadata = postMetadata ?? throw new ArgumentNullException(nameof(postMetadata));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Title => _postMetadata.Title;
        public string Summary => _postMetadata.Summary;
        public string Author => _postMetadata.Author;
        public string Image => _postMetadata.Image;
        public string LanguageCode => _postMetadata.LanguageCode;
        public DateTime PublishedOn => _postMetadata.PublishedOn;
        public string BasePath => _postMetadata.BasePath;
        public IEnumerable<string> Tags => _postMetadata.Tags ?? Enumerable.Empty<string>();
        public bool IsDraft => _postMetadata.IsDraft;
        public string Content { get; }
    }
}
