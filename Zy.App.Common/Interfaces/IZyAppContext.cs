using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Interfaces
{
    public interface IZyAppContext
    {
        long UserId { get; }

        string UserName { get; }

        string ClientId { get; }

        IDictionary<string, object> Properties { get; }
    }
}
