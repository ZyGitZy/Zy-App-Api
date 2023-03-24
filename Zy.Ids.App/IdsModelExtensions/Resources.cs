using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Zy.Ids.DAL.Entitys;

namespace Singnalr.DAL.IdentityExentions
{
    public class Resources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[] {
                new ApiResource("Zy.Api","Demo"){
                UserClaims = new List<string>{ ClaimsTypes.UserId, ClaimsTypes.UserName, ClaimsTypes.ClientName },
                ApiSecrets = {
                new Secret("Zy.Api.secret".Sha256())
                    }
                }
                };
        }

        public static IEnumerable<Client> Clients =>
       new List<Client>
       {
            new Client
            {
                ClientId = "client",

                // 没有交互式用户，使用 clientid/secret 进行身份验证
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                // 用于身份验证的密钥
                ClientSecrets =
                {
                    new Secret("secret".Sha256())  //secret加密密钥 Sha256加密方式
                },

                // 客户端有权访问的范围
                AllowedScopes = { "Zy.Api" },
                AccessTokenLifetime = 120 //过期时间，默认3600秒
            }
       };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),

                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" })
                {
                    Emphasize = true,
                },
            };
        }
    }
}
