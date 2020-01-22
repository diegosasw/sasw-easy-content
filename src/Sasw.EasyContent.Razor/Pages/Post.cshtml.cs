namespace Sasw.EasyContent.Razor.Pages
{
    using Configurations;
    using Contracts.Services;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Threading.Tasks;

    public class PostModel
        : PageModel
    {
        private readonly IBlogQueryService _blogQueryService;
        public string Title;
        public string Author;
        public HtmlString HtmlContent;
        public string PublishedOn;

        public PostModel(IBlogQueryService blogQueryService)
        {
            _blogQueryService = blogQueryService;
        }

        public async Task OnGetAsync()
        {
            var relativePath = HttpContext.Items[Constants.RequestedPathParamsKey] as string;
            var post = await _blogQueryService.GetPost(relativePath);
            Title = post.Title;
            Author = post.Author;
            HtmlContent = new HtmlString(post.Content);
            PublishedOn = post.PublishedOn.ToLongDateString();
        }
    }
}
