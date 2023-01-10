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
using Zy.App.Common.Core.DbContextExtension;
using Zy.Ids.App.IdsModelExtensions;
using Zy.Ids.Dal;

namespace Zy.Ids.App
{
    public static class IdsModelExtension
    {
        public static IMvcBuilder AddIdsModel(this IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            AddScop(mvcBuilder.Services);
            mvcBuilder.Services.AddIdentityServiceModel(configuration);
            mvcBuilder.Services.AddAutoMapperModule(new List<Assembly>
            {
                //typeof(VideoFileProfileDto).Assembly
            });
            AddControllers(mvcBuilder);
            return mvcBuilder;
        }

        private static void AddControllers(IMvcBuilder builder)
        {
            //builder.AddApplicationPart(typeof(VideoFileController).Assembly);
        }

        private static void AddScop(this IServiceCollection services)
        {
            services.AddDbContext<ZyIdsDbContext>();
            services.AddScoped<DbContext, DbContextBase>();
            services.AddScoped<DbContextBase, ZyIdsDbContext>();
        }
    }
}
