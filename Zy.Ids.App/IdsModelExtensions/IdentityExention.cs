using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Singnalr.DAL.IdentityExentions;
using System.Text;
using Zy.App.Common.Models;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;
using static System.Net.WebRequestMethods;

namespace Zy.Ids.App.IdsModelExtensions
{
    public static class IdentityExention
    {
        public static IServiceCollection AddIdentityServiceModel(this IServiceCollection service, IConfiguration configuration, string selectName = "IdentityOptions")
        {
            AddSocp(service);
            AddIdentityModel(service, opt => configuration.GetSection(selectName).Bind(opt));
            service.AddIdentityServer()
             .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Resources.GetApiResources())
            //.AddInMemoryClients(Resources.Clients)
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            .AddRedirectUriValidator<RedirectUrlValidator>()
            .AddClientStore<ClientStore>()
            .AddPersistedGrantStore<RefreshTokenStore>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddAspNetIdentity<UserEntity>()
            .Services.AddTransient<IdentityServer4.ResponseHandling.IUserInfoResponseGenerator, UserInfoResponseGenerator>()
            .AddTransient<IdentityServer4.ResponseHandling.IIntrospectionResponseGenerator, IntrospectionResponseGenerator>();

            var authority = configuration.GetSection("Authority").Value;

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.Authority = authority;
                options.RequireHttpsMetadata = false;
                options.Audience = "Signalr";
            });

            // 策略 可以限制满足某些需求后才能通过
            //service.AddAuthorization(opt =>
            //{
            //    opt.AddPolicy("ApiScop", o =>
            //    {
            //        o.RequireClaim("client_id");
            //    });
            //});

            //service.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddIdentityServerAuthentication("Bearer", options =>
            //{
            //    options.Authority = authorityc;
            //    options.RequireHttpsMetadata = false;
            //    options.ApiName = "Signalr";
            //    options.ApiSecret = "Signalr.secret";
            //});


            return service;
        }

        private static void AddIdentityModel(IServiceCollection service, Action<IdentityOptions> action)
        {
            IdentityOptions identityOptions = new();
            action.Invoke(identityOptions);
            service.AddIdentity<UserEntity, RoleEntity>(option =>
            {
                option.User.AllowedUserNameCharacters = null;
                option.Lockout.MaxFailedAccessAttempts = identityOptions.Lockout.MaxFailedAccessAttempts;
                option.Password.RequireLowercase = identityOptions.Password.RequireLowercase;
                option.Password.RequireDigit = identityOptions.Password.RequireDigit;
                option.Password.RequireNonAlphanumeric = identityOptions.Password.RequireNonAlphanumeric;
                option.Password.RequireUppercase = identityOptions.Password.RequireUppercase;
            }).AddEntityFrameworkStores<ZyUserDbContext>().AddDefaultTokenProviders();
        }

        private static void AddSocp(IServiceCollection services)
        {
            services.AddScoped<IRedirectUriValidator, RedirectUrlValidator>();
            services.AddScoped<IClientStore, ClientStore>();
            services.AddScoped<IPersistedGrantStore, RefreshTokenStore>();
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
        }
    }
}
