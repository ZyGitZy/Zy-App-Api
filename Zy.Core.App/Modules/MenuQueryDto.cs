using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.App.Modules
{
    public class MenuQueryDto : ResourceQueryDto
    {
        public string Name { get; set; } = string.Empty;

        public long RoleId { get; set; }

        public long? ParentId { get; set; }

        public bool IsGetParent { get; set; } = false;
    }
}
