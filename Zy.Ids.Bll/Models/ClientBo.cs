using Zy.App.Common.Models;

namespace Zy.Ids.Bll.Models
{
    public class ClientBo : ResourceBo
    {
        public int ConsentLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public int RefreshTokenUsage { get; set; }

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public int RefreshTokenExpiration { get; set; }

        public int AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        public bool IncludeJwtId { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public string ClientUri { get; set; } = string.Empty;

        public bool RequireClientSecret { get; set; }

        public bool AllowOfflineAccess { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }

        public bool RequireConsent { get; set; }

        public string ClientClaimsPrefix { get; set; } = string.Empty;

        public string ClientName { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public long CreateByUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public long LastUpdateByUserId { get; set; }

        public string[] ClientScopes { get; set; } = Array.Empty<string>();

        public DateTime LastUpdateDateTime { get; set; }
    }
}
