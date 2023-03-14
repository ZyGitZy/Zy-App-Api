using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.Ids.App.Controllers;
using Zy.Ids.App.JwtModelExtensions;
using Zy.Ids.App.Profiles;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Profiles;
using Zy.Ids.Bll.Services;
using Zy.Ids.Dal;

namespace Zy.Ids.App
{
    public static class JwtModelExtension
    {
        public static IZyMvcBuilder AddJwtModel(this IZyMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            mvcBuilder.AddJwt(configuration);
            mvcBuilder.AddModules(m =>
            {
                m.AddAutoMapper(typeof(ClientAppProfile).Assembly);
                m.AddAutoMapper(typeof(ClientBllProfile).Assembly);
                m.AddController(typeof(ClientController).Assembly);
                m.AddService();
            });
           
            return mvcBuilder;
        }

        private static void AddService(this IZyMvcModuleBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddDbContext<ZyIdsDbContext>();
            services.AddScoped<DbContextBase, ZyIdsDbContext>();
            services.AddScoped(typeof(IZyIdsEntityStore<>), typeof(ZyIdsEntityStore<>));
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
