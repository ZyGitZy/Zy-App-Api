using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.Bll.Modelus
{
    public class RoleMenuQueryBo : ResourceBo
    {
        public long MenuId { get; set; }
        public long RoleId { get; set; }
    }
}
