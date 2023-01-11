using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class QueryResult<T>
    {
        public int? Count { get; set; }

        public IEnumerable<T>? Items { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }

        public DateTime? QueryDateTime { get; set; }


        public QueryResult()
        {
            QueryDateTime = DateTime.Now;
        }

        public QueryResult(IQueryPaging paging)
          : this()
        {
            Offset = paging.GetValidOffset();
            Limit = paging.GetValidLimit();
        }
    }
}
