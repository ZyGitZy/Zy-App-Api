using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.Ids.App.Controllers;
using Zy.Ids.App.JwtModelExtensions;
using Zy.Ids.App.Profiles;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Profiles;
using Zy.Ids.Bll.Services;
using Zy.Ids.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.Ids.App
{
    public static class JwtModelExtension
    {
        public static IMvcCoreBuilder AddJwtModel(this IMvcCoreBuilder mvcBuilder, IConfiguration configuration)
        {
            AddScop(mvcBuilder.Services);
            mvcBuilder.Services.AddJwt(configuration);
            mvcBuilder.Services.AddAutoMapperModule(new List<Assembly>
            {
                typeof(ClientAppProfile).Assembly,
                typeof(ClientBllProfile).Assembly
            });
            AddControllers(mvcBuilder);
            return mvcBuilder;
        }

        private static void AddControllers(IMvcCoreBuilder builder)
        {
            builder.AddApplicationPart(typeof(ClientController).Assembly);
        }

        private static void AddScop(this IServiceCollection services)
        {
            services.AddDbContext<ZyIdsDbContext>();
            services.AddScoped<DbContextBase, ZyIdsDbContext>();
            services.AddScoped(typeof(IZyIdsEntityStore<>), typeof(ZyIdsEntityStore<>));
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
