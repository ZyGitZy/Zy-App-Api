using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class InternalSuperClientOptions
    {
        public string InternalHost { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public string ClientName { get; set; } = string.Empty;

        public int AccessTokentLifetime { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }
}
