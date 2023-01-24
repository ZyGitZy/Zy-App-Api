using IdentityModel;
using IdentityServer4;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Singnalr.DAL.IdentityExentions;
using System.Security.Claims;
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
            .AddRedirectUriValidator<RedirectUrlValidator>()
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            .AddInMemoryApiResources(Resources.GetApiResources())
            //.AddInMemoryClients(Resources.Clients)
            .AddClientStore<ClientStore>()
            .AddPersistedGrantStore<RefreshTokenStore>()
            .AddAspNetIdentity<UserEntity>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            //.AddTestUsers(TestUsers.Users)
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
                options.Audience = "Zy.Api";
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

    public class TestUsers
    {
        static TestUsers()
        {
            Users = new List<TestUser>
                        {
                            new TestUser
                                {
                                    SubjectId = "818727",
                                    Username = "mriza@workmail.com",
                                    Password = "alice.password",
                                    Claims =
                                        {
                                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                                            new Claim(
                                                JwtClaimTypes.Address,
                                                @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                                                IdentityServerConstants.ClaimValueTypes.Json)
                                        }
                                },
                            new TestUser
                                {
                                    SubjectId = "88421113",
                                    Username = "bob",
                                    Password = "bob.password",
                                    Claims =
                                        {
                                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                                            new Claim(
                                                JwtClaimTypes.Address,
                                                @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                                                IdentityServerConstants.ClaimValueTypes.Json),
                                            new Claim("location", "somewhere")
                                        }
                                }
                        };
        }

        public static List<TestUser> Users { get; }
    }
}
