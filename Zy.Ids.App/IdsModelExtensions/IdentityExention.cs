using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Singnalr.DAL.IdentityExentions;
using Zy.User.DAL.Entitys;

namespace Zy.Ids.App.IdsModelExtensions
{
    public static class IdentityExention
    {
        public static IServiceCollection AddIdentityServiceModel(this IServiceCollection service)
        {
            service.AddIdentityServer()
              .AddDeveloperSigningCredential(true, "./tempkey.rsa")
              .AddRedirectUriValidator<RedirectUrlValidator>()
              .AddInMemoryIdentityResources(Resources.GetIdentityResources())
              .AddInMemoryApiResources(Resources.GetApiResources())
              .AddClientStore<ClientStore>()
              .AddPersistedGrantStore<RefreshTokenStore>()
              .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
              .AddAspNetIdentity<UserEntity>()
              .Services.AddTransient<IdentityServer4.ResponseHandling.IUserInfoResponseGenerator, UserInfoResponseGenerator>()
              .AddTransient<IdentityServer4.ResponseHandling.IIntrospectionResponseGenerator, IntrospectionResponseGenerator>();


            service.AddAuthorization();
            service.AddAuthentication("Bearer").AddJwtBearer();

            return service;
        }
    }
}
