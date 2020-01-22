namespace Sasw.EasyContent.Contracts.Models
{
    public interface IPostFilter
    {
        string Tag { get; }
        int? Year { get; }
        int? Month { get; }
        string Author { get; }
        bool IncludesDraft { get; }
        string LanguageCode { get; }
    }
}