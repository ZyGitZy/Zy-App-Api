using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;

namespace Zy.App.Common.Core.DbContextExtension
{
    public static class ClaimsPrincipalExtension
    {
        public static long GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindClaimAsLong(ClaimsTypes.UserId);
        }

        public static string GetUserName(this ClaimsPrincipal claims)
        {
            return claims.FindClaim(ClaimsTypes.UserName);
        }

        public static string GetClinetId(this ClaimsPrincipal claims)
        {
            return claims.FindClaim(ClaimsTypes.ClientId);
        }

        public static string GetClinetName(this ClaimsPrincipal claims)
        {
            return claims.FindClaim(ClaimsTypes.ClientName);
        }

        public static long FindClaimAsLong(this ClaimsPrincipal claims, string claimsType)
        {
            var result = claims.FindClaim(claimsType);

            if (string.IsNullOrWhiteSpace(result))
                return 0;
            return long.Parse(result);
        }

        public static string FindClaim(this ClaimsPrincipal claims, string claimsType)
        {
            var result = claims.FindFirst(f => f.Type == claimsType);
            return result?.Value ?? "";
        }
    }
}
