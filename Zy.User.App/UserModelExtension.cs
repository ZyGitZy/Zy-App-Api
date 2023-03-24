using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.App.Abstractions;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Zy.App.Common.StoreCore;
using Zy.User.App.Controllers;
using Zy.User.App.Profiles;
using Zy.User.Bll.Interfaces;
using Zy.User.Bll.Profiles;
using Zy.User.Bll.Services;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.User.App
{
    public static class UserModelExtension
    {
        public static IZyMvcBuilder AddUserModel(this IZyMvcBuilder mvcBuilder, Action<ZyDbContextOption> opt)
        {
            var option = new ZyDbContextOption();
            opt(option);
            mvcBuilder.AddModules(m =>
            {
                m.AddServices();
                m.AddMysqlDbContext<ZyUserDbContext>(o => o.Apply(option));
                m.AddAutoMapper(typeof(UserDtoProfile).Assembly);
                m.AddAutoMapper(typeof(UserBoProfile).Assembly);
                m.AddController(typeof(UserController).Assembly);
            });

            return mvcBuilder;
        }

        private static void AddServices(this IZyMvcModuleBuilder mvcbuilder)
        {
            var services = mvcbuilder.Services;
            services.TryAddScoped(typeof(EntityStore<>), typeof(UserEntityStore<>));
            services.AddScoped(typeof(UserEntityStore<>), typeof(UserEntityStore<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
        }
    }
}
