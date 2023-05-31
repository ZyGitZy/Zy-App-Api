using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.Other.Dal;

namespace Zy.Other.App.Extensions
{
    public static class ZyOtherAppExtension
    {
        public static IZyMvcBuilder AddZyOtherApp(this IZyMvcBuilder builder, Action<ZyOtherOptionBuilder> action)
        {
            ZyOtherOptionBuilder zyOther = new();
            action(zyOther);

            builder.AddModules(m =>
            {
                m.AddMysqlDbContext<ZyOtherDbContext>(o => o.Apply(zyOther));
                m.AddServices();
                //m.AddController();
                //m.AddAutoMapper();
            });

            return builder;
        }

        public static void AddServices(this IZyMvcModuleBuilder zyMvcModuleBuilder) 
        {
        
        }
    }
}
