using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.JwtModelExtensions.JwtModelExtensionOptions
{
    public class JwtExtensionOption
    {
        public bool ValidateIssuerSigningKey { get; set; }

        public string IssuerSigningKey { get; set; } = string.Empty;

        public bool ValidateAudience { get; set; }

        public string ValidAudience { get; set; } = string.Empty;

        public string ValidIssuer { get; set; } = string.Empty;

        public bool ValidateIssuer { get; set; }

        public bool ValidateLifetime { get; set; }

        public void Apply(JwtExtensionOption option)
        {
            this.ValidateIssuerSigningKey = option.ValidateIssuerSigningKey;
            this.IssuerSigningKey = option.IssuerSigningKey;
            this.ValidateAudience = option.ValidateAudience;
            this.ValidAudience = option.ValidAudience;
            this.ValidIssuer = option.ValidIssuer;
            this.ValidateIssuer = option.ValidateIssuer;
            this.ValidateLifetime = option.ValidateLifetime;
        }
    }
}
