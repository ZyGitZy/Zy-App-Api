using Microsoft.AspNetCore.Builder;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.Ids.App;
using Zy.Ids.App.JwtModelExtensions;
using Zy.User.App;
using Zy.User.DAL.Entitys;
using Zy.Video.App;

namespace Zy.App.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddLibScopModels();
            var mvc = services.AddMvcCore().AddApiExplorer();
            //mvc.AddJwtModel(Configuration);
            mvc.AddIdsModel(Configuration);
            mvc.AddUserModel();
            mvc.AddVideoServiceModule();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

        }
    }
}
