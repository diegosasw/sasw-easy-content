namespace Sasw.EasyContent.Razor.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class PipelineExtensions
    {
        public static IApplicationBuilder UseEasyContent(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PostProcessor>();
            return builder;
        }
    }
}
