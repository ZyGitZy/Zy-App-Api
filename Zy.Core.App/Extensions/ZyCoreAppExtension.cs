using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Models;
using Zy.Core.App.Controllers;
using Zy.Core.App.Profiles;
using Zy.Core.Dal;

namespace Zy.Core.App.Extensions
{
    public static class ZyCoreAppExtension
    {
        public static IZyMvcBuilder AddZyCoreModule(this IZyMvcBuilder builder, Action<ZyCoreAppOption> action)
        {
            ZyCoreAppOption zyCoreAppOption = new();
            action(zyCoreAppOption);
            builder.AddModules(m =>
            {
                m.AddMysqlDbContext<ZyCoreDbContext>(o => o.Apply(zyCoreAppOption));
                m.AddServices();
                m.AddController(typeof(MenuController).Assembly);
                m.AddAutoMapper(typeof(MenuDtoProfile).Assembly);
            });

            return builder;
        }

        public static void AddServices(this IZyMvcModuleBuilder builder)
        {
            IServiceCollection service = builder.Services;
            service.TryAddScoped(typeof(IZyCoreEntityStore<>), typeof(ZyCoreEntityStore<>));
        }
    }
}
