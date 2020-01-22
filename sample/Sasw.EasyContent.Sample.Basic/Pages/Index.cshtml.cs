namespace Sasw.EasyContent.Sample.Basic.Pages
{
    using Builders;
    using Contracts.Models;
    using Contracts.Services;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IndexModel
        : PageModel
    {
        private readonly IBlogQueryService _blogQueryService;
        public IEnumerable<IPostSummary> PostSummaries { get; private set; }

        public IndexModel(IBlogQueryService blogQueryService)
        {
            _blogQueryService = blogQueryService;
        }
        public async Task OnGetAsync()
        {
            var postSummaries = await _blogQueryService.GetPostSummaries(new PostFilterBuilder().Build());
            PostSummaries = postSummaries;
        }
    }
}
