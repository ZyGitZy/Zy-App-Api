using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class EmptyZyAppContext : IZyAppContext
    {
        public static readonly IZyAppContext Empty = new EmptyZyAppContext();

        public long UserId { get; }

        public string UserName { get; } = string.Empty;

        public string ClientId { get; } = string.Empty;

        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
    }
}
