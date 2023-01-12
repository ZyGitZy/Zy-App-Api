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
using Zy.App.Common.StoreCore;
using Zy.User.App.Controllers;
using Zy.User.App.Profiles;
using Zy.User.Bll.Interfaces;
using Zy.User.Bll.Profiles;
using Zy.User.Bll.Services;
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
                typeof(UserDtoProfile).Assembly,
                typeof(UserBoProfile).Assembly
            });
            return mvcBuilder;
        }

        private static void AddControllers(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UserController).Assembly);
        }

        private static void AddScop(this IServiceCollection services)
        {
            services.AddDbContext<ZyUserDbContext>();
            services.AddScoped(typeof(EntityStore<>), typeof(UserEntityStore<>));
            services.AddScoped(typeof(UserEntityStore<>), typeof(UserEntityStore<>));
            services.AddScoped<IUserService, UserService>();
        }
    }
}
