namespace Sasw.EasyContent.Sample.Basic
{
    using IoCC;
    using IoCC.Options;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddEasyContent(
                sp =>
                {
                    var host = sp.GetService<IWebHostEnvironment>();
                    var fileProvider = host.WebRootFileProvider;
                    var fileProviderOptions =
                        new FileProviderOptions
                        {
                            FileProvider = fileProvider
                        };
                    return fileProviderOptions;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEasyContent();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
