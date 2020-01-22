namespace Sasw.EasyContent.Models
{
    using Contracts.Models;

    internal class PostFilter
        : IPostFilter
    {
        public string Tag { get; }
        public int? Year { get; }
        public int? Month { get; }
        public string Author { get; }
        public bool IncludesDraft { get; }
        public string LanguageCode { get; }

        internal PostFilter(string tag, int? year, int? month, string author, bool includesDraft, string languageCode)
        {
            Tag = tag;
            Year = year;
            Month = month;
            Author = author;
            IncludesDraft = includesDraft;
            LanguageCode = languageCode;
        }
    }
}
