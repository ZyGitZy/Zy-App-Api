using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Core.DbContextExtension.DbContextOptions;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Zy.Ids.App.Controllers;
using Zy.Ids.App.IdsModelExtensions;
using Zy.Ids.App.Profiles;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Profiles;
using Zy.Ids.Bll.Services;
using Zy.Ids.Dal;

namespace Zy.Ids.App
{
    public static class IdsModelExtension
    {
        public static IZyMvcBuilder AddIdsModel(this IZyMvcBuilder mvcBuilder, IConfiguration configuration, Action<ZyDbContextOption> action)
        {
            ZyDbContextOption zyDbContextOption = new();
            action(zyDbContextOption);

            mvcBuilder.AddIdentityServiceModel(configuration);

            mvcBuilder.AddModules(m =>
            {
                m.AddMysqlDbContext<ZyIdsDbContext>(opt => opt.Apply(zyDbContextOption));
                m.AddAutoMapper(typeof(ClientAppProfile).Assembly);
                m.AddAutoMapper(typeof(ClientBllProfile).Assembly);
                m.AddController(typeof(ClientController).Assembly);
                m.AddService();
            });

            return mvcBuilder;
        }

        private static void AddService(this IZyMvcModuleBuilder services)
        {
            services.Services.AddDbContext<ZyIdsDbContext>();
            services.Services.AddScoped<DbContextBase, ZyIdsDbContext>();
            services.Services.AddScoped(typeof(IZyIdsEntityStore<>), typeof(ZyIdsEntityStore<>));
            services.Services.AddScoped<IClientService, ClientService>();
        }
    }
}
