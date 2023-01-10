using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Models
{
    public class QueryResult<T>
    {
        public int? Count { get; set; }

        public IEnumerable<T>? Items { get; set; }
    }
}
