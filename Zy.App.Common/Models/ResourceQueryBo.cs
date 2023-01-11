using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class ResourceQueryBo : IQueryPaging, IResourceBo, IQueryCustomerSort
    {
        public virtual int DefaultPageSize => 100;

        public virtual bool EnableCustomerSort => true;

        public virtual bool EnablePaging => true;

        public virtual DateTime? FromDateTime { get; set; }

        public virtual int? Limit { get; set; }

        public virtual int MaxPageSize => 10000;

        public virtual int MinPageSize => 1;

        public virtual int? Offset { get; set; }

        public virtual string SortExpression { get; set; } = string.Empty;

        public virtual bool IsSortable()
        {
            if (!EnableCustomerSort)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                return false;
            }

            return true;
        }
    }
}
