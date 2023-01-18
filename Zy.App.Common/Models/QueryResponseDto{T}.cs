using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Models
{
    public class QueryResponseDto<T>
    {
        public int? Count { get; set; }

        public IEnumerable<T>? Items { get; set; }

        public int? Offset { get; set; }

        public int? Limit { get; set; }
    }
}
