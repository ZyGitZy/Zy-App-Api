using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singnalr.DAL.IdentityExentions
{
    public class Resources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[] {
                new ApiResource("Signalr","学习"){
                UserClaims = new List<string>{ "user_name" },
                ApiSecrets = {
                new Secret("Signalr.secret".Sha256())
                    }
                }
                };
        }

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
