using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.User.Bll.Models
{
    public class UserQueryBo : ResourceQueryBo
    {
        public string UserName { get; set; } = string.Empty;

        public string No { get; set; } = string.Empty;
    }
}
