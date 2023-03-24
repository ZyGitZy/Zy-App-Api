using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.User.App.Models
{
    public class UserRoleDto : ResourceDto
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}
