using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.AppExtensions
{
    public class UpdateContextInfo
    {
        public UpdateContextInfo(IZyAppContext singlarContext)
        {
            if (singlarContext == null)
            {
                throw new ArgumentNullException(nameof(singlarContext));
            }
            this.UserId = singlarContext.UserId;
            this.UserName = singlarContext.UserName;
            this.ClientId = singlarContext.ClientId;
        }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string ClientId { get; set; }
    }
}
