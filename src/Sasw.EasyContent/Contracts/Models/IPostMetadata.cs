namespace Sasw.EasyContent.Contracts.Models
{
    using System;
    using System.Collections.Generic;

    public interface IPostMetadata
    {
        string Title { get; }
        string Summary { get; }
        string Author { get; }
        string Image { get; }
        string LanguageCode { get; }
        DateTime PublishedOn { get; }
        string BasePath { get; }
        IEnumerable<string> Tags { get; }
        bool IsDraft { get; }
    }
}