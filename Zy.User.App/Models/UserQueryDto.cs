using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.User.App.Models
{
    public class UserQueryDto : ResourceQueryDto
    {
        public string UserName { get; set; } = string.Empty;

        public string No { get; set; } = string.Empty;
    }
}
