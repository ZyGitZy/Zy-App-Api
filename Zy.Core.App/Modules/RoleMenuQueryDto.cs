using System.ComponentModel.DataAnnotations;
using Zy.App.Common.Models;

namespace Zy.Core.App.Modules
{
    public class RoleMenuQueryDto : ResourceQueryDto
    {
        public long MenuId { get; set; }

        [Required(ErrorMessage = AppErrorCodes.Reqired)]
        public long RoleId { get; set; }
    }
}
