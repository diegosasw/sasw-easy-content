namespace Sasw.EasyContent.Models
{
    using Contracts.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostMetadata
        : IPostMetadata
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string LanguageCode { get; set; }
        public DateTime PublishedOn { get; set; }
        public string BasePath { get; set; }
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
        public bool IsDraft { get; set; }
    }
}