namespace Sasw.EasyContent.Razor.Pages
{
    using Configurations;
    using Contracts.Services;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Threading.Tasks;

    public class PostModel
        : PageModel
    {
        private readonly IBlogQueryService _blogQueryService;
        public string Title;
        public string Author;
        public DateTime PublishedOn;
        public HtmlString HtmlContent;

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
            PublishedOn = post.PublishedOn;
            HtmlContent = new HtmlString(post.Content);
        }
    }
}
