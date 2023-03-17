using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.Bll.Modelus
{
    public class MenuQueryBo:ResourceQueryBo
    {
        public string Name { get; set; } = string.Empty;
    }
}
