namespace Sasw.EasyContent.Builders
{
    using Contracts.Models;
    using Models;

    public class PostFilterBuilder
    {
        private string _tag;
        private int? _year;
        private int? _month;
        private string _author;
        private bool _includesDraft;
        private string _languageCode;

        public PostFilterBuilder WithTag(string tag)
        {
            _tag = tag;
            return this;
        }

        public PostFilterBuilder WithYear(int year)
        {
            _year = year;
            return this;
        }

        public PostFilterBuilder WithMonth(int month)
        {
            _month = month;
            return this;
        }

        public PostFilterBuilder WithIncludesDraft(bool includesDraft)
        {
            _includesDraft = includesDraft;
            return this;
        }

        public PostFilterBuilder WithAuthor(string author)
        {
            _author = author;
            return this;
        }

        public PostFilterBuilder WithLanguageCode(string languageCode)
        {
            _languageCode = languageCode;
            return this;
        }

        public IPostFilter Build()
        {
            var postFilter = new PostFilter(_tag, _year, _month, _author, _includesDraft, _languageCode);
            return postFilter;
        }
    }
}
