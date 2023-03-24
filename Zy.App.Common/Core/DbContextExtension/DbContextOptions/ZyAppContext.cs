using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions
{
    public class ZyAppContext : IZyAppContext
    {
        public long UserId { get; } = 0;

        public string UserName { get; } = string.Empty;

        public string ClientId { get; } = string.Empty;

        public string ClientName { get; } = string.Empty;

        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public ZyAppContext(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null || httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.User == null)
            {
                return;
            }

            var user = httpContextAccessor.HttpContext.User;

            this.UserId = user.GetUserId();

            this.UserName = user.GetUserName();

            this.ClientId = user.GetClinetId();

            this.ClientName = user.GetClinetName();
        }

        public ZyAppContext(long userId, string userName, string clientName, string clientId, IDictionary<string, object> properties)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.ClientId = clientId;
            this.Properties = properties;
            this.ClientName = clientName;
        }
    }


    public static class ClaimsTypes
    {
        public static string UserId = "user_id";

        public static string UserName = "user_name";

        public static string ClientId = "client_id";

        public static string ClientName = "client_name";
    }
}
