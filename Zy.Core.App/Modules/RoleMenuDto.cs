using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.App.Modules
{
    public class RoleMenuDto : ResourceDto
    {
        [Required(ErrorMessage = AppErrorCodes.Reqired)]
        public long MenuId { get; set; }

        [Required(ErrorMessage = AppErrorCodes.Reqired)]
        public long RoleId { get; set; }
    }
}
