using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
using Zy.User.Dal;

namespace Zy.User.App
{
    public static class UserModelExtension
    {
        public static IMvcBuilder AddUserModel(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddScop();
            AddControllers(mvcBuilder);
            mvcBuilder.Services.AddAutoMapperModule(new List<Assembly>
            {
                //typeof(VideoFileProfileDto).Assembly
            });
            return mvcBuilder;
        }

        private static void AddControllers(IMvcBuilder builder)
        {
            //builder.AddApplicationPart(typeof(VideoFileController).Assembly);
        }

        private static void AddScop(this IServiceCollection services)
        {
            services.AddDbContext<ZyUserDbContext>();
        }
    }
}
