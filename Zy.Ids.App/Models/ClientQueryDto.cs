using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Ids.App.Models
{
    public class ClientQueryDto : ResourceQueryDto
    {
        public string Scops { get; set; } = string.Empty;
    }
}
