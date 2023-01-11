using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;

namespace Zy.User.Bll.Models
{
    public class UserBo : ResourceBo
    {
        public string UserName { get; set; } = string.Empty;

        public string NormalizedUserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NormalizedEmail { get; set; } = string.Empty;

        public bool? EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public string PhoneNumber { get; set; } = string.Empty;

        public bool? PhoneNumberConfirmed { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool? LockoutEnabled { get; set; }

        public int? AccessFailedCount { get; set; }

        public ActiveStatus? Status { get; set; }

        public string No { get; set; } = string.Empty;

        public long? CreateByUserId { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public long? LastUpdateByUserId { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
