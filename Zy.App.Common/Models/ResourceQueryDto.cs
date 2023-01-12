using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Models
{
    public class ResourceQueryDto
    {
        public int? Limit { get; set; }

        public int? Offset { get; set; }

        public string? SortExpression { get; set; }

        public virtual Dictionary<string, OrderTypes>? OrderBy { get; set; }
    }

    public enum OrderTypes
    {
        Asc,

        Desc
    }
}
