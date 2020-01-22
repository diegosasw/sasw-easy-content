namespace Sasw.EasyContent.Models
{
    using Contracts.Models;
    using System;
    using System.Collections.Generic;

    public class PostSummary
        : IPostSummary
    {
        private readonly IPostMetadata _postMetadata;

        public PostSummary(IPostMetadata postMetadata, string relativePath)
        {
            _postMetadata = postMetadata ?? throw new ArgumentNullException(nameof(postMetadata));
            RelativePath = relativePath ?? throw new ArgumentNullException(nameof(relativePath));
        }

        public string Title => _postMetadata.Title;
        public string Summary => _postMetadata.Summary;
        public string Author => _postMetadata.Author;
        public string Image => _postMetadata.Image;
        public string LanguageCode => _postMetadata.LanguageCode;
        public DateTime PublishedOn => _postMetadata.PublishedOn;
        public string BasePath => _postMetadata.BasePath;
        public IEnumerable<string> Tags => _postMetadata.Tags;
        public bool IsDraft => _postMetadata.IsDraft;
        public string RelativePath { get; }
    }
}
