namespace Sasw.EasyContent.IoCC.Options
{
    public class PostOptions
    {
        public string PostRootFolder { get; set; }
        public string PostExtension { get; set; }
        public string PostViewerRoute { get; set; }

        public static PostOptions Default =>
            new PostOptions
            {
                PostExtension = ".md",
                PostRootFolder = "/blog",
                PostViewerRoute = "/post"
            };
    }
}
