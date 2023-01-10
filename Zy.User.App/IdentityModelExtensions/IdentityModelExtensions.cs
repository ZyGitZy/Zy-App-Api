using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.User.App.IdentityModelExtensions
{
    public static class IdentityModelExtension
    {
        public static IServiceCollection AddIdentityModel(this IServiceCollection service, IConfiguration configuration, string selectName = "IdentityOptions")
        {
            return AddIdentityModel(service, opt => configuration.GetSection(selectName).Bind(opt));
        }

        public static IServiceCollection AddIdentityModel(this IServiceCollection service, Action<IdentityOptions> action)
        {
            IdentityOptions identityOptions = new();
            action.Invoke(identityOptions);

            service.AddIdentity<UserEntity, RoleEntity>(option =>
            {
                option.User.AllowedUserNameCharacters = identityOptions.User.AllowedUserNameCharacters;
                option.Lockout.MaxFailedAccessAttempts = identityOptions.Lockout.MaxFailedAccessAttempts;
                option.Password.RequireLowercase = identityOptions.Password.RequireLowercase;
                option.Password.RequireDigit = identityOptions.Password.RequireDigit;
                option.Password.RequireNonAlphanumeric = identityOptions.Password.RequireNonAlphanumeric;
                option.Password.RequireUppercase = identityOptions.Password.RequireUppercase;
            }).AddEntityFrameworkStores<ZyUserDbContext>();

            return service;
        }
    }
}
