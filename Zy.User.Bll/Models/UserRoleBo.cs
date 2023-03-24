using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.User.Bll.Models
{
    public class UserRoleBo:ResourceBo
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}
