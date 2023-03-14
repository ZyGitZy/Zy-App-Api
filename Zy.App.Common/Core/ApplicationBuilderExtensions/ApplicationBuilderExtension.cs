using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.ApplicationBuilderExtensions.ApplicationBuilderOptions;

namespace Zy.App.Common.Core.ApplicationBuilderExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static IZyApplicationBuilder UseZyApplicationBuilder(this IApplicationBuilder application)
        {
            return application.UseZyApplicationBuilder(a => { });
        }

        public static IZyApplicationBuilder UseZyApplicationBuilder(this IApplicationBuilder app, Action<ZyApplicationOptions> action)
        {
            ZyApplicationOptions zyApplicationOptions = new();

            action(zyApplicationOptions);

            app.UseRouting();

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("*");
            });

            app.UseIdentityServer();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(e => e.MapDefaultControllerRoute());

            ZyApplicationBuilder applicationBuilder = new ZyApplicationBuilder(app, zyApplicationOptions);

            return applicationBuilder;
        }
    }
}
