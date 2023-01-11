using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;

namespace Zy.User.App.Models
{
    public class UserDto : ResourceDto
    {
        [DisplayName("用户名")]
        [MaxLength(PropertyLength.Name, ErrorMessage = AppErrorCodes.MaxLength)]
        [RequiredSet(ErrorMessage = AppErrorCodes.Reqired)]
        public string UserName { get; set; } = string.Empty;

        public string NormalizedUserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NormalizedEmail { get; set; } = string.Empty;

        public bool? EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [MaxLength(PropertyLength.Name, ErrorMessage = AppErrorCodes.MaxLength)]
        [RequiredSet(ErrorMessage = AppErrorCodes.Reqired)]
        public string Password { get; set; } = string.Empty;

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public string PhoneNumber { get; set; } = string.Empty;

        public bool? PhoneNumberConfirmed { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool? LockoutEnabled { get; set; }

        public int? AccessFailedCount { get; set; }

        public ActiveStatus? Status { get; set; }

        [MaxLength(PropertyLength.Name, ErrorMessage = AppErrorCodes.MaxLength)]
        [RequiredSet(ErrorMessage = AppErrorCodes.Reqired)]
        public string No { get; set; } = string.Empty;

        public long? CreateByUserId { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public long? LastUpdateByUserId { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
