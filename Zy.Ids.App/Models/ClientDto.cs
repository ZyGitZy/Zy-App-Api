using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;
using Zy.App.Common.AppExtensions;

namespace Zy.Ids.App.Models
{
    public class ClientDto : ResourceDto
    {
        public string GrantType { get; set; } = string.Empty;

        public int ConsentLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public int RefreshTokenUsage { get; set; }

        public string[] ClientScopes { get; set; } = Array.Empty<string>();

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

        [RequiredSet(ErrorMessage = AppErrorCodes.Reqired)]
        public string ClientName { get; set; } = string.Empty;

        [RequiredSet(ErrorMessage = AppErrorCodes.Reqired)]
        public string ClientId { get; set; } = string.Empty;

        public long CreateByUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public long LastUpdateByUserId { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
