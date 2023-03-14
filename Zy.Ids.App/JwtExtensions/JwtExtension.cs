using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.Ids.App.JwtModelExtensions.JwtModelExtensionOptions;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.Ids.App.JwtModelExtensions
{
    public static class JwtExtension
    {
        public static JwtExtensionBuilder AddJwt(this IZyMvcBuilder service, IConfiguration configuration, string sectionKey = "JwtOption")
        {
            return AddJwt(service, option =>
             {
                 configuration.GetSection(sectionKey).Bind(option);
             });
        }

        public static JwtExtensionBuilder AddJwt(this IZyMvcBuilder service, Action<JwtExtensionOption> option)
        {
            var jwtOption = new JwtExtensionOption();

            option.Invoke(jwtOption);

            var builder = new JwtExtensionBuilder(service.Services, jwtOption);

            builder.AddAuthentication(e => e.Apply(builder.Option));

            return builder;
        }

        public static JwtExtensionBuilder AddAuthentication(this JwtExtensionBuilder builder, Action<JwtExtensionOption> fun)
        {
            builder.Service.AddAuthentication(e =>
            {
                e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                jwt =>
                {
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    var option = new JwtExtensionOption();
                    fun(option);
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, //如果为 True，则验证签名证书。
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.IssuerSigningKey)), // 证签名证书
                        ValidAudience = option.ValidAudience, // 控制是否在令牌验证期间验证访问群体,即验证Jwt的Payload部分的aud。
                        ValidateAudience = option.ValidateAudience,
                        ValidIssuer = option.ValidIssuer, // 控制是否在令牌验证期间验证颁发者。
                        ValidateIssuer = option.ValidateIssuer,
                        ValidateLifetime = option.ValidateLifetime // 控制是否在令牌验证期间验证生存期。
                    };
                });

            builder.Service.AddAuthorization();

            builder.Service.AddIdentity<UserEntity, RoleEntity>().AddEntityFrameworkStores<ZyUserDbContext>().AddDefaultTokenProviders();

            return builder;
        }
    }
}
