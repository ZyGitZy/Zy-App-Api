using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Ids.Bll.Models
{
    public class ClientQueryBo: ResourceQueryBo
    {
        public string Scops { get; set; } = string.Empty;
    }
}
