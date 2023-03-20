using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.Bll.Modelus
{
    public class RoleMenuBo : ResourceBo
    {
        public long MenuId { get; set; }

        public long RoleId { get; set; }
    }
}
